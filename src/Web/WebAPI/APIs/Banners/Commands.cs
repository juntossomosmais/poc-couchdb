using Contracts.Services.Banner;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Banners.Validators;

namespace WebAPI.APIs.Banners;

public static class Commands
{
    public record CreateBanner(IBus Bus, Payloads.CreateBanner Payload, CancellationToken CancellationToken)
        : Validatable<CreateBannerValidator>, ICommand<Command.CreateBanner>
    {
        public Command.CreateBanner Command
            => new(Payload.Title, Payload.ImagePath, Payload.Order, Payload.CallToAction, Payload.Author);
    }
    
    public record DeleteBanner(IBus Bus, Guid BannerId, CancellationToken CancellationToken)
        : Validatable<DeleteBannerValidator>, ICommand<Command.DeleteBanner>
    {
        public Command.DeleteBanner Command => new(BannerId);
    }
}