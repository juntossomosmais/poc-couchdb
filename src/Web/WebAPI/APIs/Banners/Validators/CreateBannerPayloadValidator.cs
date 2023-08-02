using FluentValidation;

namespace WebAPI.APIs.Banners.Validators;

public class CreateBannerPayloadValidator : AbstractValidator<Payloads.CreateBanner>
{
    public CreateBannerPayloadValidator()
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