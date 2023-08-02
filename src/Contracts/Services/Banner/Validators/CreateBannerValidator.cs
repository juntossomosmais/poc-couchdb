using FluentValidation;

namespace Contracts.Services.Banner.Validators;

public class CreateBannerValidator : AbstractValidator<Command.CreateBanner>
{
    public CreateBannerValidator()
    {
        RuleFor(banner => banner.Title)
            .NotEmpty();
        
        RuleFor(banner => banner.ImagePath)
            .NotEmpty();
        
        RuleFor(banner => banner.Order)
            .GreaterThan(0);
        
        RuleFor(banner => banner.CallToAction)
            .NotEmpty();
        
        RuleFor(banner => banner.Author)
            .NotEmpty();
    }
}