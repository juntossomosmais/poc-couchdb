using FluentValidation;

namespace WebAPI.APIs.Banners.Validators;

public class DeactivateBannerValidator : AbstractValidator<Commands.DeactivateBanner>
{
    public DeactivateBannerValidator()
    {
        RuleFor(request => request.BannerId)
            .NotEmpty();
    }
}