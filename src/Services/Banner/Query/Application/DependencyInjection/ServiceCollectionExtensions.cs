using Application.UseCases.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services.AddScoped<IProjectBannerDetailsWhenBannerChangedInteractor, ProjectBannerDetailsWhenBannerChangedInteractor>();
    
}