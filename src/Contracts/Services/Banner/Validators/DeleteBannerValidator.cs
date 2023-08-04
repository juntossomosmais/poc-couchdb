using FluentValidation;

namespace Contracts.Services.Banner.Validators;

public class DeleteBannerValidator : AbstractValidator<Command.DeleteBanner>
{
    public DeleteBannerValidator()
    {
        RuleFor(request => request.BannerId)
            .NotNull()
            .NotEmpty();
    }
}