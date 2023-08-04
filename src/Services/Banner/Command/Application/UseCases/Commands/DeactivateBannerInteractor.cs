using Application.Abstractions;
using Application.Services;
using Contracts.Services.Banner;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class DeactivateBannerInteractor : IInteractor<Command.DeactivateBanner>
{
    private readonly IApplicationService _applicationService;

    public DeactivateBannerInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.DeactivateBanner command, CancellationToken cancellationToken)
    {
        var banner = await _applicationService.LoadAggregateAsync<Banner>(command.BannerId, cancellationToken);
        banner.Handle(command);
        await _applicationService.AppendEventsAsync(banner, cancellationToken);
    }
}