using FluentValidation;
using labo1_opdracht1.Models;

namespace labo1_opdracht1.Validators;

public class WineValidator : AbstractValidator<Wine>
{
    public WineValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(5).WithMessage("Naam moet minstens 5 characters lang zijn");
        RuleFor(p => p.Year)
            .GreaterThan(1900).WithMessage("Ongeldig jaar")
            .LessThan(2022).WithMessage("ongeldig jaar");
    }
}