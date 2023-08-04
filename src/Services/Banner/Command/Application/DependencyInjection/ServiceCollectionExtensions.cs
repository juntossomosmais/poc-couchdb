using Application.Abstractions;
using Application.Services;
using Application.UseCases.Commands;
using Contracts.Services.Banner;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddScoped<IApplicationService, ApplicationService>();

    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.CreateBanner>, CreateBannerInteractor>()
            .AddScoped<IInteractor<Command.DeleteBanner>, DeleteBannerInteractor>()
            .AddScoped<IInteractor<Command.ActivateBanner>, ActivateBannerInteractor>()
            .AddScoped<IInteractor<Command.DeactivateBanner>, DeactivateBannerInteractor>();
}