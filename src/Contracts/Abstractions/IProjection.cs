namespace Contracts.Abstractions;

public interface IProjection
{
    bool IsDeleted { get; }
    long Version { get; }
}