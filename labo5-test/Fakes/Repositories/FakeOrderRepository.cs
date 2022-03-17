using System.Collections.Generic;
using System.Threading.Tasks;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_test.Fakes.Repositories;

public class FakeOrderRepository : IOrderRepository
{
    public Task<List<Order>> GetOrders()
    {
        throw new System.NotImplementedException();
    }
}