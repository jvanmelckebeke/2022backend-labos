namespace labo2_opdracht1.Models;

public class VaccineRegistration
{
    public Guid VaccineRegistrationId { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? EMail { get; set; }
    public int YearOfBirth { get; set; }
    public string? VaccinationDate { get; set; }
    public Guid VaccineTypeId { get; set; }
    public Guid VaccinationLocationId { get; set; }
}