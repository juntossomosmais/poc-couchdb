using Contracts.Abstractions.Messages;

namespace Application.Abstractions;

public interface IInteractor<in TMessage>
    where TMessage : IMessage
{
    Task InteractAsync(TMessage command, CancellationToken cancellationToken);
}