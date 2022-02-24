using FluentValidation;
using labo1_opdracht2.Models;

namespace labo1_opdracht2.Validation;

public class BrandValidator : AbstractValidator<Brand>
{
    public BrandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MinimumLength(2);
        RuleFor(p => p.Country)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(30);
        RuleFor(p => p.Logo).NotEmpty().Matches("^(http|https)://").WithMessage("logo is not a url");
    }
}