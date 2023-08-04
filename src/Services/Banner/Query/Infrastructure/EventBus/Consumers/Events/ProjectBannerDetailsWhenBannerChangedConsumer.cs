using Application.UseCases.Events;
using Contracts.Services.Banner;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectBannerDetailsWhenBannerChangedConsumer : 
    IConsumer<DomainEvent.BannerCreated>,
    IConsumer<DomainEvent.BannerDeleted>,
    IConsumer<DomainEvent.BannerActivated>,
    IConsumer<DomainEvent.BannerDeactivated>
{
    private readonly IProjectBannerDetailsWhenBannerChangedInteractor _interactor;
    
    public ProjectBannerDetailsWhenBannerChangedConsumer(IProjectBannerDetailsWhenBannerChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.BannerCreated> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.BannerDeleted> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.BannerActivated> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.BannerDeactivated> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}