using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using labo2_opdracht1.Configuration;
using labo2_opdracht1.Models;
using Microsoft.Extensions.Options;

namespace labo2_opdracht1.Repositories;

public interface IVaccineTypeRepository
{
    List<VaccineType> GetVaccineTypes();
}

public class VaccineTypeRepository : IVaccineTypeRepository
{
    private List<VaccineType> _vaccineTypes = new List<VaccineType>();

    public VaccineTypeRepository()
    {
        if (!(_vaccineTypes.Any()))
        {
            _vaccineTypes.AddRange(
                new[]
                {
                    new VaccineType
                    {
                        VaccineTypeId = Guid.Parse("4e2a72fb-f4fa-416f-87cd-ea338b518519"),
                        Name = "BioNTech, Pfizer"
                    },
                    new VaccineType
                    {
                        VaccineTypeId = Guid.Parse("7fa73e42-77d6-4a5b-aef6-0d36779bc989"),
                        Name = "Moderna"
                    }
                });
        }
    }

    public List<VaccineType> GetVaccineTypes()
    {
        return _vaccineTypes.ToList<VaccineType>();
    }
}

public class CsvVaccineTypeRepository : IVaccineTypeRepository
{
    private ICsvRepository<VaccineType> _csvRepository;

    private string _location;

    public CsvVaccineTypeRepository(IOptions<CsvConfig> csvSettings, ICsvRepository<VaccineType> csvRepository)
    {
        _csvRepository = csvRepository;
        _location = csvSettings.Value.VaccinsPath;
    }

    public List<VaccineType> GetVaccineTypes()
    {
        return _csvRepository.ReadRecordsFromCsv(_location);
    }
}