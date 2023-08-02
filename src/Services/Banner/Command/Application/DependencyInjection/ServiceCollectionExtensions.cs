using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddScoped<IApplicationService, ApplicationService>();

    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services;

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services;
}