using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace labo2_opdracht1.Repositories;

public interface ICsvRepository<T>
{
    List<T> ReadRecordsFromCsv(string location);

    void AddRecordToCsv(string location, T recordToAdd);
}

public class CsvRepository<T> : ICsvRepository<T>
{
    private static CsvConfiguration _csvConfiguration = new(CultureInfo.InvariantCulture)
    {
        NewLine = Environment.NewLine,
        Delimiter = ";"
    };

    public List<T> ReadRecordsFromCsv(string location)
    {
        using var reader = new StreamReader(location);
        using var csv = new CsvReader(reader, _csvConfiguration);
        return csv.GetRecords<T>().ToList();
    }

    public void AddRecordToCsv(string location, T recordToAdd)
    {
        List<T> registrations = ReadRecordsFromCsv(location);
        registrations.Add(recordToAdd);

        using var writer = new StreamWriter(location);
        using var csv = new CsvWriter(writer, _csvConfiguration);
        csv.WriteRecords(registrations.AsEnumerable());
    }
}