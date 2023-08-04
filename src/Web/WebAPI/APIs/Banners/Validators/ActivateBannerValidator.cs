using FluentValidation;

namespace WebAPI.APIs.Banners.Validators;

public class ActivateBannerValidator : AbstractValidator<Commands.ActivateBanner>
{
    public ActivateBannerValidator()
    {
        RuleFor(request => request.BannerId)
            .NotEmpty();
    }
}