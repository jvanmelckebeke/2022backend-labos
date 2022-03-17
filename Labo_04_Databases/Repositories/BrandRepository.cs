namespace Backend_Labo_01_Cars.Repositories;

public interface IBrandRepository
{
    Task<Brand> GetBrand(string id);

    Task<List<Brand>> GetAllBrands();

    Task<Brand> AddBrand(Brand brand);

    Task<Brand> UpdateBrand(Brand brand);

    Task DeleteBrand(string id);
}

public class BrandRepository : IBrandRepository
{
    private readonly IMongoContext _mongoContext;
    private readonly IMongoCollection<Brand> _collection;

    public BrandRepository(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
        _collection = _mongoContext.BrandsCollection;
    }

    public async Task<Brand> GetBrand(string id)
    {
        return await _collection.Find(brand => brand.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Brand>> GetAllBrands()
    {
        return await _collection.Find(brand => true).ToListAsync();
    }

    public async Task<Brand> AddBrand(Brand brand)
    {
        brand.CreatedOn = DateTime.Now;
        await _collection.InsertOneAsync(brand);
        return brand;
    }

    public async Task DeleteBrand(string id)
    {
        try
        {
            await _collection.DeleteOneAsync(brand => brand.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Brand> UpdateBrand(Brand brand)
    {
        var filter = Builders<Brand>.Filter.Eq("Id", brand.Id);
        var update = Builders<Brand>.Update
            .Set("Country", brand.Country)
            .Set("Name", brand.Name)
            .Set("Logo", brand.Logo);

        var result = await _collection.UpdateOneAsync(filter, update);
        return await GetBrand(brand.Id);
    }
}