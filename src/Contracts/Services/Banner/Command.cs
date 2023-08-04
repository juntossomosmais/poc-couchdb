using Contracts.Abstractions.Messages;

namespace Contracts.Services.Banner;

public static class Command
{
    public record CreateBanner(Guid BannerId, string Title, string ImagePath, int Order, string CallToAction, string Author) : Message, ICommand;
    
    public record ActivateBanner(Guid BannerId) : Message, ICommand;
    
    public record DeactivateBanner(Guid BannerId) : Message, ICommand;
    
    public record DeleteBanner(Guid BannerId) : Message, ICommand;
}