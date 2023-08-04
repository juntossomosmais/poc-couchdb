using Application.Abstractions;
using Application.Services;
using Contracts.Services.Banner;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class ActivateBannerInteractor : IInteractor<Command.ActivateBanner>
{
    private readonly IApplicationService _applicationService;

    public ActivateBannerInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.ActivateBanner command, CancellationToken cancellationToken)
    {
        var banner = await _applicationService.LoadAggregateAsync<Banner>(command.BannerId, cancellationToken);
        banner.Handle(command);
        await _applicationService.AppendEventsAsync(banner, cancellationToken);
    }
}