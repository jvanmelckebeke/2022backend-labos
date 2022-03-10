using Backend_Labo_01_Cars.Models;

namespace Backend_Labo_01_Cars.Repositories;

interface IBrandRepository
{
    Brand GetBrand(string id);

    List<Brand> GetBrands();

    Brand AddBrand(Brand brand);

    Brand UpdateBrand(Brand brand);
}

public class BrandRepository : ICarRepository
{
    public Car AddCar(Car car)
    {
        throw new NotImplementedException();
    }

    public Car GetCar(string id)
    {
        throw new NotImplementedException();
    }

    public List<Car> GetAllCars()
    {
        throw new NotImplementedException();
    }
}