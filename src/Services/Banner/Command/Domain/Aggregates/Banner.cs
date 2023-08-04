using Contracts.Abstractions.Messages;
using Contracts.Services.Banner;
using Contracts.Services.Banner.Validators;
using Domain.Abstractions.Aggregates;
using Domain.Enumerations;
using FluentValidation;

namespace Domain.Aggregates;

public class Banner : AggregateRoot<BannerValidator>
{
    public string? Title { get; private set; }
    public string? ImagePath { get; private set; }
    public string? CallToAction { get; private set; }
    public string? Author { get; private set; }
    public int Order { get; private set; }
    public BannerStatus Status { get; private set; } = BannerStatus.Inactive;
    
    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.CreateBanner cmd)
    {
        new CreateBannerValidator().ValidateAndThrow(cmd);
        
        RaiseEvent<DomainEvent.BannerCreated>(version
            => new DomainEvent.BannerCreated(Guid.NewGuid(), cmd.Title, cmd.ImagePath, cmd.Order, cmd.CallToAction, cmd.Author, BannerStatus.Inactive, version));
    }
    
    private void Handle(Command.DeleteBanner cmd)
    {
        new DeleteBannerValidator().ValidateAndThrow(cmd);
        
        if (IsDeleted) return;
        
        RaiseEvent<DomainEvent.BannerDeleted>(version => new DomainEvent.BannerDeleted(cmd.BannerId, version));
    }

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);
    
    private void When(DomainEvent.BannerCreated @event)
        => (Id, Title, ImagePath, Order, CallToAction, Author, Status, _) = @event;
    
    private void When(DomainEvent.BannerDeleted _)
        => IsDeleted = true;
}