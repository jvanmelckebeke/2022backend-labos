using labo5_sneakers.Context;
using labo5_sneakers.Models;
using MongoDB.Driver;

namespace labo5_sneakers.Repositories;

public interface IBrandRepository
{
    Task<List<Brand>> GetBrands();

    Task<List<Brand>> AddBrands(List<Brand> brands);
}

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<Brand> _collection;

    public BrandRepository(IMongoContext context)
    {
        _collection = context.BrandsCollection;
    }
    
    public async Task<List<Brand>> GetBrands()
    {
        return await _collection.Find(brand => true).ToListAsync();
    }


    public async Task<List<Brand>> AddBrands(List<Brand> brands)
    {
        var brandList = brands.ToList();
        await _collection.InsertManyAsync(brandList);
        return brandList;
    }
}