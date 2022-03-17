using System.Collections.Generic;
using System.Threading.Tasks;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_test.Fakes.Repositories;

public class FakeSneakerRepository : ISneakerRepository
{
    public Task<List<Sneaker>> GetSneakers()
    {
        throw new System.NotImplementedException();
    }

    public Task<Sneaker> GetSneakerBySneakerId(string sneakerId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Sneaker> AddSneaker(Sneaker sneaker)
    {
        throw new System.NotImplementedException();
    }
}