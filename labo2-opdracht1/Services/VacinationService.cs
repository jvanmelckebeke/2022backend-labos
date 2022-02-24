using System.Globalization;
using labo2_opdracht1.Models;
using labo2_opdracht1.Repositories;

namespace labo2_opdracht1.Services;

public interface IVaccinationService
{
    VaccineRegistration AddRegistration(VaccineRegistration registration);
    List<VaccinationLocation> GetLocations();
    List<VaccineRegistration> GetRegistrations();
    List<VaccineType> GetVaccines();
}

public class VaccinationService : IVaccinationService
{
    private IVaccinationLocationRepository _locationRepository;
    private IVaccineTypeRepository _typeRepository;
    private IVaccineRegistrationRepository _registrationRepository;

    public VaccinationService(IVaccineRegistrationRepository vaccineRegistrationRepository,
        IVaccinationLocationRepository vaccinationLocationRepository,
        IVaccineTypeRepository vaccineTypeRepository)
    {
        _locationRepository = vaccinationLocationRepository;
        _typeRepository = vaccineTypeRepository;
        _registrationRepository = vaccineRegistrationRepository;
    }

    public VaccineRegistration AddRegistration(VaccineRegistration registration)
    {
        return _registrationRepository.AddRegistration(registration);
    }

    public List<VaccinationLocation> GetLocations()
    {
        return _locationRepository.GetLocations();
    }

    public List<VaccineRegistration> GetRegistrations()
    {
        return _registrationRepository.GetRegistrations();
    }

    public List<VaccineType> GetVaccines()
    {
        return _typeRepository.GetVaccineTypes();
    }
}