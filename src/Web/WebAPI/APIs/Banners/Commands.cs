using Contracts.Services.Banner;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Banners.Validators;

namespace WebAPI.APIs.Banners;

public static class Commands
{
    public record Create(IBus Bus, Payloads.Create Payload, CancellationToken CancellationToken)
        : Validatable<CreateBannerValidator>, ICommand<Command.CreateBanner>
    {
        public Command.CreateBanner Command
            => new(Command.Title, Command.ImagePath, Command.Order, Command.CallToAction, Command.Author);
    }
}