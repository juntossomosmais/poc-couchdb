using Contracts.Abstractions.Messages;
using Contracts.JsonConverters;
using Contracts.Services.Banner.Protobuf;
using CorrelationId.Abstractions;
using CorrelationId.HttpClient;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using MassTransit;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebAPI.DependencyInjection.Options;
using WebAPI.PipeObservers;

namespace WebAPI.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services)
        => services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            cfg.UsingRabbitMq((context, bus) =>
            {
                var options = context.GetRequiredService<IOptionsMonitor<MessageBusOptions>>().CurrentValue;

                bus.Host(options.ConnectionString);

                bus.UseMessageRetry(retry
                    => retry.Incremental(
                        retryLimit: options.RetryLimit,
                        initialInterval: options.InitialInterval,
                        intervalIncrement: options.IntervalIncrement));

                bus.UseNewtonsoftJsonSerializer();

                bus.ConfigureNewtonsoftJsonSerializer(settings =>
                {
                    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                    settings.Converters.Add(new DateOnlyJsonConverter());

                    return settings;
                });

                bus.ConfigureNewtonsoftJsonDeserializer(settings =>
                {
                    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                    settings.Converters.Add(new DateOnlyJsonConverter());

                    return settings;
                });

                bus.ConnectReceiveObserver(new LoggingReceiveObserver());
                bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                bus.ConnectSendObserver(new LoggingSendObserver());
                bus.ConfigureEndpoints(context);

                bus.ConfigureSend(pipe => pipe.AddPipeSpecification(
                    new MassTransit.Configuration.DelegatePipeSpecification<SendContext<ICommand>>(ctx =>
                    {
                        var accessor = context.GetRequiredService<ICorrelationContextAccessor>();
                        ctx.CorrelationId = new(accessor.CorrelationContext.CorrelationId);
                    })));
            });
        });

    public static void AddBannerGrpcClient(this IServiceCollection services)
        => services.AddGrpcClient<BannerService.BannerServiceClient, BannerGrpcClientOptions>();

    private static void AddGrpcClient<TClient, TOptions>(this IServiceCollection services)
        where TClient : ClientBase
        where TOptions : class
        => services.AddGrpcClient<TClient>((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<TOptions>>().CurrentValue as dynamic;
                client.Address = new(options.BaseAddress);
            })
            .AddCorrelationIdForwarding()
            .ConfigureChannel(options =>
                {
                    options.Credentials = ChannelCredentials.Insecure;
                    options.ServiceConfig = new() { LoadBalancingConfigs = { new RoundRobinConfig() } };
                }
            )
            .ConfigurePrimaryHttpMessageHandler(() =>
                new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                    EnableMultipleHttp2Connections = true
                })
            .EnableCallContextPropagation();

    public static OptionsBuilder<MessageBusOptions> ConfigureMessageBusOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<MessageBusOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<MassTransitHostOptions> ConfigureMassTransitHostOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<MassTransitHostOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<RabbitMqTransportOptions> ConfigureRabbitMqTransportOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<RabbitMqTransportOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<BannerGrpcClientOptions> ConfigureBannerGrpcClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<BannerGrpcClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}