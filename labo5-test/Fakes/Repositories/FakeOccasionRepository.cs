using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_test.Fakes.Repositories;

public class FakeOccasionRepository : IOccasionRepository
{
    private static List<Occasion> _occasions = new();

    public async Task<List<Occasion>> GetOccasions()
    {
        return await Task.FromResult(_occasions);
    }

    public async Task<List<Occasion>> AddOccasions(List<Occasion> occasions)
    {
        for (var i = 0; i < occasions.Count; i++)
        {
            occasions[i].OccasionId = Guid.NewGuid().ToString();
        }

        _occasions.AddRange(occasions);
        return await Task.FromResult(occasions);
    }
}