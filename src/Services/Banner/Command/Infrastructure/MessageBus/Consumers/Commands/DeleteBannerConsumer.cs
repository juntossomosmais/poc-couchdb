using Application.Abstractions;
using Contracts.Services.Banner;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class DeleteBannerConsumer : Consumer<Command.DeleteBanner>
{
    public DeleteBannerConsumer(IInteractor<Command.DeleteBanner> interactor)
        : base(interactor) { }
}