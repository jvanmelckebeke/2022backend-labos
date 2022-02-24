using FluentValidation;
using FluentValidation.AspNetCore;
using labo1_opdracht2.Models;
using labo1_opdracht2.Validation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CarModelValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BrandValidator>());

var app = builder.Build();

var audiBrand = new Brand()
{
    BrandId = 0,
    Name = "Audi",
    Country = "Germany",
    Logo =
        "https://audimediacenter-a.akamaihd.net/system/production/media/1282/images/bde751ee18fe149036c6b47d7595f6784f8901f8/AL090142_full.jpg"
};

var volkswagenBrand = new Brand()
{
    BrandId = 1,
    Name = "Volkswagen",
    Country = "Germany",
    Logo = "https://content.presspage.com/uploads/1397/1920_01-vw-logo-795576.jpeg?10000"
};
var mcLarenBrand = new Brand()
{
    BrandId = 2,
    Name = "McLaren",
    Country = "England",
    Logo = "https://logos-world.net/wp-content/uploads/2021/09/McLaren-Logo.png"
};
List<Brand> brands = new List<Brand>
{
    audiBrand,
    volkswagenBrand,
    mcLarenBrand
};

List<CarModel> carModels = new List<CarModel>
{
    new()
    {
        CarModelId = 0,
        Name = "Golf",
        Brand = volkswagenBrand,
        BrandId = volkswagenBrand.BrandId
    },
    new()
    {
        CarModelId = 1,
        Name = "Polo",
        Brand = volkswagenBrand,
        BrandId = volkswagenBrand.BrandId
    },
    new()
    {
        CarModelId = 2,
        Name = "A3",
        Brand = audiBrand,
        BrandId = audiBrand.BrandId
    },
    new()
    {
        CarModelId = 3,
        Name = "A5",
        Brand = audiBrand,
        BrandId = audiBrand.BrandId
    },
    new()
    {
        CarModelId = 4,
        Name = "720S",
        Brand = mcLarenBrand,
        BrandId = mcLarenBrand.BrandId
    },
};

app.MapGet("/brands", (string? country) =>
{
    if (country == null)
    {
        return Results.Ok(brands);
    }

    var bc = brands.Where(b => b.Country.ToLower().Equals(country.ToLower())).ToList();
    return Results.Ok(bc);
});

app.MapGet("/brands/{brandId}", (int brandId) =>
{
    var brand = brands.Find(b => b.BrandId == brandId);

    return brand == null ? Results.NotFound() : Results.Ok(brand);
});

app.MapPost("/brands", (IValidator<Brand> validator, Brand brand) =>
{
    var validationResult = validator.Validate(brand);

    if (validationResult.IsValid)
    {
        brand.BrandId = brands.Count + 1;
        brands.Add(brand);
        return Results.Created($"/brands/{brand.BrandId}", brand);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new {errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
});

app.MapGet("/cars", (string? country) =>
{
    if (country == null)
    {
        return Results.Ok(carModels);
    }

    var cars = carModels.Where(c => c.Brand.Country.ToLower().Equals(country.ToLower())).ToList();

    return cars.Count == 0 ? Results.NotFound() : Results.Ok(cars);
});

app.MapGet("/brands/{brandId}/cars", (int brandId) =>
{
    var cars = carModels.Where(c => c.BrandId == brandId).ToList();

    return cars.Count == 0 ? Results.NotFound() : Results.Ok(cars);
});

app.MapGet("/cars/{modelId}", (int modelId) =>
{
    var car = carModels.Find(c => c.CarModelId == modelId);

    return car == null ? Results.NotFound() : Results.Ok(car);
});


app.Run("http://localhost:3000");