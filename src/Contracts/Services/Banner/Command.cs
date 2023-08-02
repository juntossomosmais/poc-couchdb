using Contracts.Abstractions.Messages;

namespace Contracts.Services.Banner;

public static class Command
{
    public record CreateBanner(string Title, string ImagePath, int Order, string CallToAction, string Author) : Message, ICommand;
    
    public record UpdateBanner(Guid BannerId, string Title, string ImagePath, int Order, string CallToAction, string Author, bool Status) : Message, ICommand;
    
    public record DeleteBanner(Guid BannerId) : Message, ICommand;
}