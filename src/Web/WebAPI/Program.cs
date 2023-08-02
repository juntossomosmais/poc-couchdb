using System.Reflection;
using System.Text.Json.Serialization;
using Contracts.JsonConverters;
using CorrelationId;
using CorrelationId.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using WebAPI.APIs.Banners;
using WebAPI.DependencyInjection.Extensions;
using WebAPI.DependencyInjection.Options;

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
    services.AddProblemDetails();

    services.AddCors(options
        => options.AddDefaultPolicy(policyBuilder
            => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));

    services.AddDefaultCorrelationId(options =>
    {
        options.RequestHeader =
            options.ResponseHeader =
                options.LoggingScopeKey = "CorrelationId";
        options.UpdateTraceIdentifier = true;
        options.AddToLoggingScope = true;
    });

    services
        .AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters()
        .AddValidatorsFromAssemblyContaining<Program>();
    
    builder.Services.Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.Converters.Add(new DateOnlyTextJsonConverter());
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    services
        .AddSwaggerGenNewtonsoftSupport()
        .AddFluentValidationRulesToSwagger()
        .AddEndpointsApiExplorer()
        .AddSwagger();

    services
        .AddApiVersioning(options => options.ReportApiVersions = true)
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

    services.AddHealthChecks();

    services.AddMessageBus();
    services.AddBannerGrpcClient();

    services.ConfigureMessageBusOptions(
        context.Configuration.GetSection(nameof(MessageBusOptions)));

    services.ConfigureBannerGrpcClientOptions(
        context.Configuration.GetSection(nameof(BannerGrpcClientOptions)));

    services.ConfigureMassTransitHostOptions(
        context.Configuration.GetSection(nameof(MassTransitHostOptions)));

    services.ConfigureRabbitMqTransportOptions(
        context.Configuration.GetSection(nameof(RabbitMqTransportOptions)));

    services.AddHttpLogging(options
        => options.LoggingFields = HttpLoggingFields.All);
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseCorrelationId();
app.UseCors();
app.UseSerilogRequestLogging();

app.MapHealthChecks("/healthz");

app.NewVersionedApi("Banners").MapBannerApiV1();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
    app.ConfigureSwagger();

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


public partial class Program { }