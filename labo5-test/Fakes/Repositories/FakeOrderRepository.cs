using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_test.Fakes.Repositories;

public class FakeOrderRepository : IOrderRepository
{
    private List<Order> _orders = new();

    public async Task<List<Order>> GetOrders()
    {
        return await Task.FromResult(_orders);
    }

    public async Task<Order> AddOrder(Order order)
    {
        order.OrderId = Guid.NewGuid().ToString();
        _orders.Add(order);

        return await Task.FromResult(order);
    }
}