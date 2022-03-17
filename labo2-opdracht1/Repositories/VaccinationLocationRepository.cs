using System.Globalization;
using CsvHelper;
using labo2_opdracht1.Configuration;
using labo2_opdracht1.Models;
using Microsoft.Extensions.Options;

namespace labo2_opdracht1.Repositories;

public interface IVaccinationLocationRepository
{
    List<VaccinationLocation> GetLocations();

    VaccinationLocation FindOneByLocationId(Guid locationId);
}

public class VaccinationLocationRepository : IVaccinationLocationRepository
{
    private static List<VaccinationLocation> _locations = new List<VaccinationLocation>();

    public VaccinationLocationRepository()
    {
        if (!(_locations.Any()))
        {
            _locations.Add(new VaccinationLocation()
            {
                VaccinationLocationId = Guid.Parse("2774e3d1-2b0f-47ab-b391-8ea43e6f9d80"),
                Name = "Kortrijk Expo"
            });
            _locations.Add(new VaccinationLocation()
            {
                VaccinationLocationId = Guid.Parse("0bb537ea-8209-422f-a9e1-2c1e37d0cb4d"),
                Name = "Gent Expo"
            });
        }
    }

    public List<VaccinationLocation> GetLocations()
    {
        return _locations.ToList<VaccinationLocation>();
    }

    public VaccinationLocation FindOneByLocationId(Guid locationId)
    {
        return GetLocations().Find(l => l.VaccinationLocationId == locationId);
    }
}

public class CsvVaccinationLocationRepository : IVaccinationLocationRepository
{
    private ICsvRepository<VaccinationLocation> _csvRepository;
    private string _location;

    public CsvVaccinationLocationRepository(
        IOptions<CsvConfig> csvSettings,
        ICsvRepository<VaccinationLocation> csvRepository)
    {
        _csvRepository = csvRepository;
        _location = csvSettings.Value.LocationPath;
    }

    public List<VaccinationLocation> GetLocations()
    {
        return _csvRepository.ReadRecordsFromCsv(_location);
    }
    
    public VaccinationLocation FindOneByLocationId(Guid locationId)
    {
        return GetLocations().Find(l => l.VaccinationLocationId == locationId);
    }
}