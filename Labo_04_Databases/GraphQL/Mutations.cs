using Backend_Labo_01_Cars.GraphQL.Brands;
using Backend_Labo_01_Cars.GraphQL.Cars;

namespace Backend_Labo_01_Cars.GraphQL;

public class Mutations
{
    public async Task<BrandPayload> AddBrand([Service] ICarService carService, AddBrandInput input)
    {
        var newBrand = new Brand()
        {
            Name = input.Name,
            Country = input.Country
        };
        var created = await carService.AddBrand(newBrand);
        return new BrandPayload(created);
    }


    public async Task<BrandPayload> UpdateBrand([Service] ICarService carService, UpdateBrandInput input)
    {
        var oldBrand = await carService.GetBrand(input.Id);

        oldBrand.Name = input.Name;
        oldBrand.Country = input.Country;

        var result = await carService.UpdateBrand(oldBrand);

        return new BrandPayload(result);
    }

    public async Task DeleteBrand([Service] ICarService carService, DeleteBrandInput input)
    {
        await carService.DeleteBrand(input.Id);
    }
    
    public async Task<CarPayload> AddCar([Service] ICarService carService, AddCarInput input)
    {
        var newCar = new Car()
        {
            Name = input.Name,
            Brand = input.Brand
        };
        var created = await carService.AddCar(newCar);
        return new CarPayload(created);
    }
    
    public async Task<CarPayload> UpdateCar([Service] ICarService carService, UpdateCarInput input)
    {
        var oldCar = await carService.GetCar(input.Id);

        oldCar.Name = input.Name;
        oldCar.Brand = input.Brand;
        
        var result = await carService.UpdateCar(oldCar);

        return new CarPayload(result);
    }
}