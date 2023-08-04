using Application.Abstractions;
using Contracts.Services.Banner;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ActivateBannerConsumer : Consumer<Command.ActivateBanner>
{
    public ActivateBannerConsumer(IInteractor<Command.ActivateBanner> interactor)
        : base(interactor) { }
}