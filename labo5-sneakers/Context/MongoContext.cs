using labo5_sneakers.Configuration;
using labo5_sneakers.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace labo5_sneakers.Context;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Sneaker> SneakerCollection { get; }
    IMongoCollection<Brand> BrandsCollection { get; }
    IMongoCollection<Occasion> OccasionCollection { get; }
    IMongoCollection<Order> OrdersCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get { return _client; }
    }

    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Sneaker> SneakerCollection => _database.GetCollection<Sneaker>(_settings.SneakerCollection);

    public IMongoCollection<Brand> BrandsCollection => _database.GetCollection<Brand>(_settings.BrandsCollection);

    public IMongoCollection<Occasion> OccasionCollection => _database.GetCollection<Occasion>(_settings.OccasionCollection);

    public IMongoCollection<Order> OrdersCollection => _database.GetCollection<Order>(_settings.OrdersCollection);
}