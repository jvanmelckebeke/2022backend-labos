using labo5_sneakers.Context;
using labo5_sneakers.Models;
using MongoDB.Driver;

namespace labo5_sneakers.Repositories;

public interface ISneakerRepository
{
    Task<List<Sneaker>> GetSneakers();

    Task<Sneaker> GetSneakerBySneakerId(string sneakerId);

    Task<Sneaker> AddSneaker(Sneaker sneaker);
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
}