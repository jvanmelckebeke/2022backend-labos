using Backend_Labo_01_Cars.GraphQL.Cars;

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");


builder.Services.Configure<DatabaseSettings>(mongoSettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();

builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ICarRepository, CarRepository>();

builder.Services.AddTransient<ICarService, CarService>();

builder.Services
    .AddGraphQLServer()
    .AddFiltering()
    .AddSorting()
    .AddType<BrandType>()
    .AddType<CarType>()
    .AddMutationType<Mutations>()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);


var app = builder.Build();
app.MapGraphQL();

app.MapGet("/", () => "Hello World");

app.MapGet("/setup", async (ICarService service) =>
{
    await service.SetupDummyData();
    return Results.Created("/cars", null);
});

app.MapPost("/brands", async (ICarService service, Brand brand) =>
{
    var result = await service.AddBrand(brand);
    return Results.Created($"/brand/{result.Id}", result);
});


app.MapPost("/cars", async (ICarService service, Car car) =>
{
    var result = await service.AddCar(car);
    return Results.Created($"/cars/{result.Id}", result);
});

app.MapGet("/brands", async (ICarService service) => Results.Ok(await service.GetBrands()));

app.MapGet("/cars", async (ICarService service) => Results.Ok(await service.GetCars()));

app.MapGet("/cars/{id}", async (ICarService service, string id) =>
{
    var car = await service.GetCar(id);
    return car.Name == null ? Results.NotFound() : Results.Ok(car);
});

app.MapGet("/brands/{id}", async (ICarService service, string id) =>
{
    var brand = await service.GetBrand(id);
    return brand.Name == null ? Results.NotFound() : Results.Ok(brand);
});

//GraphQL.SchemaExtensions.RegisterTypeMapping<Brand,BrandType>();
app.Run("http://0.0.0.0:3000");