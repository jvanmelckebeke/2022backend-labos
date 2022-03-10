using Backend_Labo_01_Cars.DataContext;
using Backend_Labo_01_Cars.Models;
using MongoDB.Driver;

namespace Backend_Labo_01_Cars.Repositories;

interface IBrandRepository
{
    Task<Brand> GetBrand(string id);

    Task<List<Brand>> GetBrands();

    Task<Brand> AddBrand(Brand brand);

    Brand UpdateBrand(Brand brand);
}

public class BrandRepository : IBrandRepository
{
    private readonly MongoContext _mongoContext;
    private readonly IMongoCollection<Brand> _collection;

    public BrandRepository(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
        _collection = _mongoContext.BrandsCollection;
    }

    public async Task<Brand> GetBrand(string id)
    {
        return (await _collection.FindAsync(brand => brand.Id == id)).First();
    }

    public async Task<List<Brand>> GetBrands()
    {
        return (await _collection.FindAsync(brand => true)).ToList();
    }

    public async Task<Brand> AddBrand(Brand brand)
    {
        await _collection.InsertOneAsync(brand);
        return brand;
    }

    public Brand UpdateBrand(Brand brand)
    {
        throw new NotImplementedException();
    }
}