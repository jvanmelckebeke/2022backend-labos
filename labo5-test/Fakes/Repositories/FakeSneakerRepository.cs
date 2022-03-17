using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenDonut;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_test.Fakes.Repositories;

public class FakeSneakerRepository : ISneakerRepository
{
    private List<Sneaker> _sneakers = new List<Sneaker>();

    public async Task<List<Sneaker>> GetSneakers()
    {
        return await Task.FromResult(_sneakers);
    }

    public async Task<Sneaker> GetSneakerBySneakerId(string sneakerId)
    {
        return await Task.FromResult(_sneakers.Find(sneaker => sneaker.SneakerId == sneakerId));
    }

    public async Task<Sneaker> AddSneaker(Sneaker sneaker)
    {
        sneaker.SneakerId ??= Guid.NewGuid().ToString();

        _sneakers.Add(sneaker);
        return await Task.FromResult(sneaker);
    }

    public async Task<Sneaker> UpdateSneaker(Sneaker sneaker)
    {
        for (int i = 0; i < _sneakers.Count; i++)
        {
            if (_sneakers[i].SneakerId == sneaker.SneakerId)
            {
                _sneakers[i] = sneaker;
                return await Task.FromResult(_sneakers[i]);
            }
        }


        return await Task.FromResult<Sneaker>(null);
    }

    public async Task<List<Sneaker>> AddSneakers(List<Sneaker> sneakers)
    {
        _sneakers.AddRange(sneakers);
        return await Task.FromResult(sneakers);
    }
}