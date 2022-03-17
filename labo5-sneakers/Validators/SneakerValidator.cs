using FluentValidation;
using labo5_sneakers.Models;

namespace labo5_sneakers.Validators;

public class SneakerValidator : AbstractValidator<Sneaker>
{
    public SneakerValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("name should be filled");
        RuleFor(s => s.Price)
            .GreaterThan(0).WithMessage("price should be positive");
        RuleFor(s => s.Occasions)
            .NotEmpty().WithMessage("there should be at least one occasion");
        RuleFor(s => s.Brand)
            .NotEmpty().WithMessage("brand should be filled");
    }
}