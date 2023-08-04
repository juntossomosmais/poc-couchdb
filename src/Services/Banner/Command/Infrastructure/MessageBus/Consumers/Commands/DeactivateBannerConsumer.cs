using Application.Abstractions;
using Contracts.Services.Banner;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class DeactivateBannerConsumer : Consumer<Command.DeactivateBanner>
{
    public DeactivateBannerConsumer(IInteractor<Command.DeactivateBanner> interactor)
        : base(interactor) { }
}