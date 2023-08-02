using Application.Abstractions;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.EventBus.Abstractions;


public abstract class Consumer<TMessage> : IConsumer<TMessage>
    where TMessage : class, IEvent
{
    private readonly IInteractor<TMessage> _interactor;

    protected Consumer(IInteractor<TMessage> interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<TMessage> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}