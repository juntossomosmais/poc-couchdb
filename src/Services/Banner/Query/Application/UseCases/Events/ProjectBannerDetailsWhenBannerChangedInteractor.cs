using Application.Abstractions;
using Contracts.Services.Banner;

namespace Application.UseCases.Events;


public interface IProjectBannerDetailsWhenBannerChangedInteractor :
    IInteractor<DomainEvent.BannerCreated>,
    IInteractor<DomainEvent.BannerDeleted>,
    IInteractor<DomainEvent.BannerActivated>,
    IInteractor<DomainEvent.BannerDeactivated> { }

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

        await _projectionGateway.AddOrUpdateAsync(userDetails, cancellationToken);
    }

    public async Task InteractAsync(DomainEvent.BannerDeleted @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.BannerId.ToString(), cancellationToken);

    public async Task InteractAsync(DomainEvent.BannerActivated @event, CancellationToken cancellationToken)
    {
        var banner = await _projectionGateway.GetAsync(@event.BannerId.ToString(), cancellationToken);
        
        banner.Status = 1;
        banner.Version = @event.Version;
        
        await _projectionGateway.AddOrUpdateAsync(banner, cancellationToken);
    }

    public async Task InteractAsync(DomainEvent.BannerDeactivated @event, CancellationToken cancellationToken)
    {
        var banner = await _projectionGateway.GetAsync(@event.BannerId.ToString(), cancellationToken);
        
        banner.Status = 0;
        banner.Version = @event.Version;
        
        await _projectionGateway.AddOrUpdateAsync(banner, cancellationToken);
    }
}