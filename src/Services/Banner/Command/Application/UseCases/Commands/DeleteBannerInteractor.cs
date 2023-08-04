using Application.Abstractions;
using Application.Services;
using Contracts.Services.Banner;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class DeleteBannerInteractor : IInteractor<Command.DeleteBanner>
{
    private readonly IApplicationService _applicationService;

    public DeleteBannerInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.DeleteBanner command, CancellationToken cancellationToken)
    {
        var banner = await _applicationService.LoadAggregateAsync<Banner>(command.BannerId, cancellationToken);
        banner.Handle(command);
        await _applicationService.AppendEventsAsync(banner, cancellationToken);
    }
}