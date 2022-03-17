namespace Backend_Labo_01_Cars.DataContext;

public interface IMongoContext
{
    IMongoClient Client { get; }

    IMongoDatabase Database { get; }

    IMongoCollection<Car> CarsCollection { get; }

    IMongoCollection<Brand> BrandsCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    private readonly DatabaseSettings _settings;

    public IMongoClient Client => _client;
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Car> CarsCollection => _database.GetCollection<Car>(_settings.CarsCollection);

    public IMongoCollection<Brand> BrandsCollection => _database.GetCollection<Brand>(_settings.BrandsCollection);
}