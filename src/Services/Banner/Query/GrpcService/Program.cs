using System.Reflection;
using Application.DependencyInjection;
using GrpcService;
using Infrastructure.EventBus.DependencyInjection.Extensions;
using Infrastructure.EventBus.DependencyInjection.Options;
using Infrastructure.Projections.DependencyInjection.Extensions;
using Infrastructure.Projections.DependencyInjection.Options;
using MassTransit;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((context, provider) =>
{
    provider.ValidateScopes =
        provider.ValidateOnBuild =
            context.HostingEnvironment.IsDevelopment();
});

builder.Configuration
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();

builder.Host.ConfigureServices((context, services) =>
{
    services.AddCors(options
        => options.AddDefaultPolicy(policyBuilder
            => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));

    services.AddGrpc();
    services.AddEventBus();
    services.AddMessageValidators();
    services.AddProjections();
    services.AddInteractors();

    services.ConfigureEventBusOptions(
        context.Configuration.GetSection(nameof(EventBusOptions)));

    services.ConfigureMassTransitHostOptions(
        context.Configuration.GetSection(nameof(MassTransitHostOptions)));
    
    services.ConfigureCouchDbOptions(
        context.Configuration.GetSection(nameof(CouchDbOptions)));

    services.AddHttpLogging(options
        => options.LoggingFields = HttpLoggingFields.All);
});

var app = builder.Build();

app.UseCors();
app.UseSerilogRequestLogging();
app.MapGrpcService<BannerGrpcService>();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}