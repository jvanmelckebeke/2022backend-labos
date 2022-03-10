using Backend_Labo_01_Cars.Models;

namespace Backend_Labo_01_Cars.Repositories;

interface ICarRepository
{
    Car AddCar(Car car);

    Car GetCar(string id);

    List<Car> GetAllCars();
}

public class CarRepository : ICarRepository
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