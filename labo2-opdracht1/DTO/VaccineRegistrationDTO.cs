namespace labo2_opdracht1.DTO;

public class VaccineRegistrationDTO
{
    public Guid VaccinationRegistrationId { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? EMail { get; set; }
    public int YearOfBirth { get; set; }
    public string? VaccinationDate { get; set; }
    public string? VaccineName { get; set; }
    public string? Location { get; set; }
}