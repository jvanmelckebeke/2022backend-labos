using labo5_sneakers.Context;
using labo5_sneakers.Models;
using MongoDB.Driver;

namespace labo5_sneakers.Repositories;

public interface IOccasionRepository
{
    Task<List<Occasion>> GetOccasions();
    Task<List<Occasion>> AddOccasions(List<Occasion> occasions);
}

public class OccasionRepository : IOccasionRepository
{
    private readonly IMongoCollection<Occasion> _collection;

    public OccasionRepository(IMongoContext context)
    {
        _collection = context.OccasionCollection;
    }
    
    public async Task<List<Occasion>> GetOccasions()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<Occasion>> AddOccasions(List<Occasion> occasions)
    {
        await _collection.InsertManyAsync(occasions);
        return occasions;
    }
}