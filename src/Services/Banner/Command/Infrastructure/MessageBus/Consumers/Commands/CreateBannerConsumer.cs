using Application.Abstractions;
using Contracts.Services.Banner;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class CreateBannerConsumer : Consumer<Command.CreateBanner>
{
    public CreateBannerConsumer(IInteractor<Command.CreateBanner> interactor)
        : base(interactor) { }
}