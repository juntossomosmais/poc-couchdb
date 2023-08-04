using FluentValidation;

namespace WebAPI.APIs.Banners.Validators;

public class DeleteBannerValidator : AbstractValidator<Commands.DeleteBanner>
{
    public DeleteBannerValidator()
    {
        RuleFor(request => request.BannerId)
            .NotNull()
            .NotEmpty();
    }
}