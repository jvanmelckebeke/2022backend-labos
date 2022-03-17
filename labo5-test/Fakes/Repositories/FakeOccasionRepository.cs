using System.Collections.Generic;
using System.Threading.Tasks;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_test.Fakes.Repositories;

public class FakeOccasionRepository : IOccasionRepository
{
    public Task<List<Occasion>> GetOccasions()
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Occasion>> AddOccasions(List<Occasion> occasions)
    {
        throw new System.NotImplementedException();
    }
}