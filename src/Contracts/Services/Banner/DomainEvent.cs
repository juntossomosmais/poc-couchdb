using Contracts.Abstractions.Messages;

namespace Contracts.Services.Banner;

public static class DomainEvent
{
    public record BannerCreated(Guid BannerId, string Title, string ImagePath, int Order, string CallToAction, string Author, int Status, long Version) : Message, IDomainEvent;
    
    public record BannerUpdated(Guid BannerId, string Title, string ImagePath, int Order, string CallToAction, string Author, bool Status, long Version) : Message, IDomainEvent;
    
    public record BannerDeleted(Guid BannerId, long Version) : Message, IDomainEvent;
}