using Infrastructure.MessageBus.DependencyInjection.Options;
using MassTransit.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

public static class OpenTelemetryExtensions
{
    public static void AddOpenTelemetryExtension(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptionsMonitor<DistributedTracingOptions>>().CurrentValue;
        
        void ConfigureResource(ResourceBuilder r) => r
            .AddService(serviceName: options.ServiceName,
                serviceVersion: typeof(OpenTelemetryExtensions).Assembly.GetName().Version?.ToString() ?? "unknown",
                serviceInstanceId: Environment.MachineName)
            .AddTelemetrySdk()
            .AddEnvironmentVariableDetector()
            .AddAttributes(new Dictionary<string, object>
            {
                ["environment.name"] = "development",
                ["team.name"] = "backend"
            });
        
        services
            .AddOpenTelemetry()
            .ConfigureResource(ConfigureResource)
            .WithTracing(p =>
            {
                p.AddSource(DiagnosticHeaders.DefaultListenerName, options.ServiceName, "MassTransit")
                    .AddAspNetCoreInstrumentation(opt =>
                    {
                        opt.RecordException = true;
                    })
                    .AddMassTransitInstrumentation()
                    .SetSampler(new AlwaysOnSampler())
                    .AddJaegerExporter(opt =>
                    {
                        opt.AgentHost = options.Host;
                        opt.AgentPort = options.Port;
                    });
            });
        
        services.Configure<AspNetCoreInstrumentationOptions>(opt => opt.RecordException = true);
        services.Configure<OpenTelemetryLoggerOptions>(opt =>
        {
            opt.IncludeScopes = true;
            opt.ParseStateValues = true;
            opt.IncludeFormattedMessage = true;
        });
    }
}