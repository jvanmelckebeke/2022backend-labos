using System.Collections.Generic;
using System.Threading.Tasks;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_test.Fakes.Repositories;

public class FakeBrandRepository : IBrandRepository
{
    public static List<Brand> _brands = new();
    public Task<Brand> AddBrand(Brand brand)
    {
        _brands.Add(brand);
        return Task.FromResult(brand);
    }

    public Task<List<Brand>> AddBrands(List<Brand> brands)
    {
        _brands.AddRange(brands);
        return Task.FromResult(brands);
    }

    public Task<List<Brand>> GetBrands()
    {
        return Task.FromResult(_brands);
    }
}