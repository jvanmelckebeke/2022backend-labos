namespace Backend_Labo_01_Cars.Services;

interface ICarService
{
    Task SetupDummyData();

    Task<List<Car>> GetCars();
    Task<Car> GetCar(string id);

    Task<List<Brand>> GetBrands();
    Task<Brand> GetBrand(string id);

    Task<Car> AddCar(Car car);
    Task<Brand> AddBrand(Brand brand);
}

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IBrandRepository _brandRepository;

    public CarService(ICarRepository carRepository, IBrandRepository brandRepository)
    {
        _carRepository = carRepository;
        _brandRepository = brandRepository;
    }

    public async Task SetupDummyData()
    {
        if (!(await _brandRepository.GetAllBrands()).Any())
        {
            var brands = new List<Brand>
            {
                new()
                {
                    Country = "Germany", Name = "Volkswagen"
                },
                new()
                {
                    Country = "Germany", Name = "BMW"
                },
                new()
                {
                    Country = "Germany", Name = "Audi"
                },
                new()
                {
                    Country = "USA", Name = "Tesla"
                }
            };

            foreach (var brand in brands)
                await _brandRepository.AddBrand(brand);
        }

        if (!(await _carRepository.GetAllCars()).Any())
        {
            var brands = await _brandRepository.GetAllBrands();
            var cars = new List<Car>
            {
                new()
                {
                    Name = "ID.3",
                    Brand = brands[0],
                },
                new()
                {
                    Name = "ID.4",
                    Brand = brands[0],
                },
                new()
                {
                    Name = "IX3",
                    Brand = brands[1],
                },
                new()
                {
                    Name = "E-Tron",
                    Brand = brands[2],
                },
                new()
                {
                    Name = "Model Y",
                    Brand = brands[3],
                }
            };
            foreach (var car in cars)
                await _carRepository.AddCar(car);
        }
    }

    public async Task<List<Car>> GetCars()
    {
        return await _carRepository.GetAllCars();
    }

    public async Task<Car> GetCar(string id)
    {
        return await _carRepository.GetCar(id);
    }

    public async Task<List<Brand>> GetBrands()
    {
        return await _brandRepository.GetAllBrands();
    }

    public async Task<Brand> GetBrand(string id)
    {
        return await _brandRepository.GetBrand(id);
    }

    public async Task<Car> AddCar(Car car)
    {
        return await _carRepository.AddCar(car);
    }

    public async Task<Brand> AddBrand(Brand brand)
    {
        return await _brandRepository.AddBrand(brand);
    }
}