using Application.Abstractions;
using Contracts.Services.Banner;

namespace Application.UseCases.Events;


public interface IProjectBannerDetailsWhenBannerChangedInteractor :
    IInteractor<DomainEvent.BannerCreated>,
    IInteractor<DomainEvent.BannerDeleted> { }

public class ProjectBannerDetailsWhenBannerChangedInteractor : IProjectBannerDetailsWhenBannerChangedInteractor
{
    private readonly IProjectionGateway<BannerDetails> _projectionGateway;
    
    public ProjectBannerDetailsWhenBannerChangedInteractor(IProjectionGateway<BannerDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }
    
    public async Task InteractAsync(DomainEvent.BannerCreated @event, CancellationToken cancellationToken)
    {
        BannerDetails userDetails = new(
            @event.BannerId,
            @event.Title,
            @event.ImagePath,
            @event.Order,
            @event.CallToAction,
            @event.Author,
            @event.Status,
            false,
            @event.Version);

        await _projectionGateway.ReplaceInsertAsync(userDetails, cancellationToken);
    }

    public async Task InteractAsync(DomainEvent.BannerDeleted @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.BannerId.ToString(), cancellationToken);
}