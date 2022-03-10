namespace Backend_Labo_01_Cars.Repositories;

public interface ICarRepository
{
    Task<Car> AddCar(Car car);

    Task<Car> GetCar(string id);

    Task<List<Car>> GetAllCars();
    Task<List<Car>> GetCarsByBrandId(string brandId);
    Task<Car> UpdateCar(Car car);
}

public class CarRepository : ICarRepository
{
    private readonly IMongoContext _mongoContext;
    private readonly IMongoCollection<Car> _collection;

    public CarRepository(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
        _collection = _mongoContext.CarsCollection;
    }

    public async Task<Car> AddCar(Car car)
    {
        car.CreatedOn = DateTime.Now;
        await _collection.InsertOneAsync(car);
        return car;
    }

    public async Task<Car> GetCar(string id)
    {
        return await _collection.Find(car => car.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Car>> GetAllCars()
    {
        return await _collection.Find(car => true).ToListAsync();
    }

    public async Task<List<Car>> GetCarsByBrandId(string brandId)
    {
        return await _collection.Find(c => c.Brand.Id == brandId).ToListAsync();
    }

    public async Task<Car> UpdateCar(Car car)
    {
        var filter = Builders<Car>.Filter.Eq("Id", car.Id);
        var update = Builders<Car>.Update
            .Set("Name", car.Name);

        var result = await _collection.UpdateOneAsync(filter, update);
        return await GetCar(car.Id);
    }
}