using labo5_sneakers.Context;
using labo5_sneakers.Models;
using MongoDB.Driver;

namespace labo5_sneakers.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetOrders();

    Task<Order> AddOrder(Order order);

}

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection;

    public OrderRepository(IMongoContext context)
    {
        _collection = context.OrdersCollection;
    }
    
    public async Task<List<Order>> GetOrders()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Order> AddOrder(Order order)
    {
        await _collection.InsertOneAsync(order);

        return order;
    }
}