using FluentValidation;

namespace WebAPI.APIs.Banners.Validators;

public class CreateBannerValidator : AbstractValidator<Commands.CreateBanner>
{
    public CreateBannerValidator()
    {
        RuleFor(request => request.Payload)
            .SetValidator(new CreateBannerPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}