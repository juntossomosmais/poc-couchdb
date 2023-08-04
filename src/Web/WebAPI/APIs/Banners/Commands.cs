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
            => new(Guid.NewGuid(), Payload.Title, Payload.ImagePath, Payload.Order, Payload.CallToAction, Payload.Author);
    }
    
    public record ActivateBanner(IBus Bus, Guid BannerId, CancellationToken CancellationToken)
        : Validatable<ActivateBannerValidator>, ICommand<Command.ActivateBanner>
    {
        public Command.ActivateBanner Command => new(BannerId);
    }
    
    public record DeactivateBanner(IBus Bus, Guid BannerId, CancellationToken CancellationToken)
        : Validatable<DeactivateBannerValidator>, ICommand<Command.DeactivateBanner>
    {
        public Command.DeactivateBanner Command => new(BannerId);
    }
    
    public record DeleteBanner(IBus Bus, Guid BannerId, CancellationToken CancellationToken)
        : Validatable<DeleteBannerValidator>, ICommand<Command.DeleteBanner>
    {
        public Command.DeleteBanner Command => new(BannerId);
    }
}