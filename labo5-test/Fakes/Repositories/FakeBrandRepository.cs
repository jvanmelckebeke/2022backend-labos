using System.Collections.Generic;
using System.Threading.Tasks;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_test.Fakes.Repositories;

public class FakeBrandRepository : IBrandRepository
{
    public Task<List<Brand>> GetBrands()
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Brand>> AddBrands(IEnumerable<Brand> brands)
    {
        throw new System.NotImplementedException();
    }
}