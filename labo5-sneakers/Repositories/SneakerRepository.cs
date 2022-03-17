using labo5_sneakers.Context;
using labo5_sneakers.Models;
using MongoDB.Driver;

namespace labo5_sneakers.Repositories;

public interface ISneakerRepository
{
    Task<List<Sneaker>> GetSneakers();

    Task<Sneaker> GetSneakerBySneakerId(string sneakerId);

    Task<Sneaker> AddSneaker(Sneaker sneaker);

    Task<Sneaker> UpdateSneaker(Sneaker sneaker);
    Task<List<Sneaker>> AddSneakers(List<Sneaker> sneakers);
}

public class SneakerRepository : ISneakerRepository
{
    private readonly IMongoCollection<Sneaker> _collection;

    public SneakerRepository(IMongoContext context)
    {
        _collection = context.SneakerCollection;
    }

    public async Task<List<Sneaker>> GetSneakers()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Sneaker> GetSneakerBySneakerId(string sneakerId)
    {
        return await _collection.Find(s => s.SneakerId == sneakerId).FirstOrDefaultAsync();
    }

    public async Task<Sneaker> AddSneaker(Sneaker sneaker)
    {
        await _collection.InsertOneAsync(sneaker);
        return sneaker;
    }
    
    public async Task<List<Sneaker>> AddSneakers(List<Sneaker> sneakers)
    {
        await _collection.InsertManyAsync(sneakers);
        return sneakers;
    }

    public async Task<Sneaker> UpdateSneaker(Sneaker sneaker)
    {
        var filter = Builders<Sneaker>.Filter.Eq("SneakerId", sneaker.SneakerId);
        var update = Builders<Sneaker>.Update
            .Set("Name", sneaker.Name)
            .Set("Stock", sneaker.Stock);

        var result = await _collection.UpdateOneAsync(filter, update);
        return await GetSneakerBySneakerId(sneaker.SneakerId);
    }
}