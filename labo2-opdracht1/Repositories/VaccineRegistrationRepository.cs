using System.Collections;
using System.Globalization;
using CsvHelper;
using labo2_opdracht1.Configuration;
using labo2_opdracht1.Models;
using Microsoft.Extensions.Options;

namespace labo2_opdracht1.Repositories;

public interface IVaccineRegistrationRepository
{
    List<VaccineRegistration> GetRegistrations();

    VaccineRegistration AddRegistration(VaccineRegistration registration);
}

public class VaccineRegistrationRepository : IVaccineRegistrationRepository
{
    private static List<VaccineRegistration> _registrations = new List<VaccineRegistration>();

    public VaccineRegistrationRepository()
    {
        if (_registrations.Any()) return;


        string textCSV =
            "d2a79300-cdad-44b1-96bd-560361d04cf1;De Preester;Dieter;dieter@preconsult.be;1978;1/1/2022;4e2a72fb-f4fa-416f-87cd-ea338b518519;2774e3d1-2b0f-47ab-b391-8ea43e6f9d80\n0e2fa67e-e808-4a6d-af7a-f87cb47d85ee;De Preester;Dieter;dieter@preconsult.be;1956;1/1/2022;4e2a72fb-f4fa-416f-87cd-ea338b518519;2774e3d1-2b0f-47ab-b391-8ea43e6f9d80\nd12a550f-2eaf-4bf8-b392-a110b7fd2328;De Preester;Dieter;dieter@preconsult.be;1985;1/1/2022;4e2a72fb-f4fa-416f-87cd-ea338b518519;2774e3d1-2b0f-47ab-b391-8ea43e6f9d80\n885c93ee-b123-4b96-92e4-b4ea9deae204;De Preester;Dieter;dieter@preconsult.be;1999;1/1/2022;4e2a72fb-f4fa-416f-87cd-ea338b518519;2774e3d1-2b0f-47ab-b391-8ea43e6f9d80\n7f191c46-f264-4710-8c80-e17d6b563d09;Poetin;Vladimir;vladi@outlook.com;1952;1/1/202;4e2a72fb-f4fa-416f-87cd-ea338b518519;0bb537ea-8209-422f-a9e1-2c1e37d0cb4d";

        foreach (string line in textCSV.Split("\n"))
        {
            string[] data = line.Split(";");

            var registration = new VaccineRegistration()
            {
                VaccineRegistrationId = Guid.Parse(data[0]),
                Name = data[1],
                FirstName = data[2],
                EMail = data[3],
                YearOfBirth = Int32.Parse(data[4]),
                VaccinationDate = data[5],
                VaccineTypeId = Guid.Parse(data[6]),
                VaccinationLocationId = Guid.Parse(data[7])
            };

            _registrations.Add(registration);
        }
    }


    public List<VaccineRegistration> GetRegistrations()
    {
        return _registrations.ToList<VaccineRegistration>();
    }

    public VaccineRegistration AddRegistration(VaccineRegistration registration)
    {
        _registrations.Add(registration);

        return registration;
    }
}

public class CsvVaccineRegistrationRepository : IVaccineRegistrationRepository
{
    private ICsvRepository<VaccineRegistration> _csvRepository;
    private string _location;

    public CsvVaccineRegistrationRepository(
        IOptions<CsvConfig> csvSettings,
        ICsvRepository<VaccineRegistration> csvRepository)
    {
        _location = csvSettings.Value.RegistrationsPath;
        _csvRepository = csvRepository;
    }

    public List<VaccineRegistration> GetRegistrations()
    {
        return _csvRepository.ReadRecordsFromCsv("csv/Registrations.csv");
    }

    public VaccineRegistration AddRegistration(VaccineRegistration registration)
    {
        _csvRepository.AddRecordToCsv("csv/Registrations.csv", registration);

        return registration;
    }
}