using Backend_Labo_01_Cars.Models;
using DotNetCore.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoContext = Backend_Labo_01_Cars.DataContext.MongoContext;

namespace Backend_Labo_01_Cars.Repositories;

interface ICarRepository
{
    Task<Car> AddCar(Car car);

    Task<Car> GetCar(string id);

    Task<List<Car>> GetAllCars();
}

public class CarRepository : ICarRepository
{
    private readonly MongoContext _mongoContext;
    private readonly IMongoCollection<Car> _collection;

    public CarRepository(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
        _collection = _mongoContext.CarsCollection;
    }

    public async Task<Car> AddCar(Car car)
    {
        await _collection.InsertOneAsync(car);
        return car;
    }

    public async Task<Car> GetCar(string id)
    {
        return (await _collection.FindAsync(car => car.Id == id)).First();
    }

    public async Task<List<Car>> GetAllCars()
    {
        return (await _collection.FindAsync(car => true)).ToList();
    }
}