using FluentValidation;
using labo2_opdracht1.Models;

namespace labo2_opdracht1.Validators;

public class RegistrationValidator : AbstractValidator<VaccineRegistration>
{
    public RegistrationValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(1);
        RuleFor(p => p.EMail).NotEmpty().EmailAddress();
        RuleFor(p => p.FirstName).NotEmpty().MinimumLength(1);
        RuleFor(p => p.VaccinationDate).NotEmpty().Matches("^[0-3]?[0-9]/1?[0-9]/[0-9]{4}$");
        RuleFor(p => p.VaccinationLocationId).NotEmpty();
        RuleFor(p => p.VaccineTypeId).NotEmpty();
        RuleFor(p => p.YearOfBirth).NotEmpty().GreaterThan(1900).LessThan(DateTime.Now.Year);
    }
}