using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface IEvent : IMessage { }

[ExcludeFromTopology]
public interface IVersionedEvent : IEvent
{
    long Version { get; }
}

[ExcludeFromTopology]
public interface IDomainEvent : IVersionedEvent { }
