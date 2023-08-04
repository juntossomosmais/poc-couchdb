using Application.Abstractions;
using Application.Services;
using Contracts.Services.Banner;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class CreateBannerInteractor : IInteractor<Command.CreateBanner>
{
    private readonly IApplicationService _applicationService;

    public CreateBannerInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    
    public async Task InteractAsync(Command.CreateBanner command, CancellationToken cancellationToken)
    {
        Banner banner = new();
        banner.Handle(command);
        await _applicationService.AppendEventsAsync(banner, cancellationToken);
    }
}