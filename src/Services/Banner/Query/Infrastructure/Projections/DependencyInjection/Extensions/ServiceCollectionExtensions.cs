using Application.Abstractions;
using Infrastructure.Projections.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Projections.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddProjections(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProjectionGateway<>), typeof(ProjectionGateway<>));
    }
    
    
    public static OptionsBuilder<CouchDbOptions> ConfigureCouchDbOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<CouchDbOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}