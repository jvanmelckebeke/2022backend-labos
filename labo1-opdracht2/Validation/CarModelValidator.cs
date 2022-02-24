using FluentValidation;
using labo1_opdracht2.Models;

namespace labo1_opdracht2.Validation;

public class CarModelValidator : AbstractValidator<CarModel>
{
    public CarModelValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2);
        RuleFor(p => p.Brand).InjectValidator();
    }
}