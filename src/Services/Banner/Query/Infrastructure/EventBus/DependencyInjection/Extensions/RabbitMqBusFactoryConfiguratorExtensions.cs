using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        //cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenChangedConsumer, DomainEvent.CartCreated>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IReceiveConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"banner.query-stack.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                if (endpoint is IRabbitMqReceiveEndpointConfigurator rabbitMq) rabbitMq.Bind<TEvent>();
                if (endpoint is IInMemoryReceiveEndpointConfigurator inMemory) inMemory.Bind<TEvent>();

                endpoint.ConfigureConsumeTopology = false;
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}