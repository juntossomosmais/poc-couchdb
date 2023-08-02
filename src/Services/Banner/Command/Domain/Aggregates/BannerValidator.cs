using FluentValidation;

namespace Domain.Aggregates;

public class BannerValidator : AbstractValidator<Banner>
{
    public BannerValidator()
    {
        RuleFor(banner => banner.Id)
            .NotEmpty();
    }
}