using FluentValidation;
using FluentValidation.AspNetCore;
using labo5_sneakers.Configuration;
using labo5_sneakers.Context;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;
using labo5_sneakers.Services;

var builder = WebApplication.CreateBuilder(args);

var dbsettings = builder.Configuration.GetSection("MongoConnection");

builder.Services.Configure<DatabaseSettings>(dbsettings);


builder.Services.AddTransient<IMongoContext, MongoContext>();

builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ISneakerRepository, SneakerRepository>();
builder.Services.AddTransient<IOccasionRepository, OccasionRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddTransient<ISneakerService, SneakerService>();
builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<SneakerRepository>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapSwagger();
app.UseSwaggerUI();


app.MapGet("/", () => "Hello World!");

app.MapGet("/setup", async (ISneakerService service) =>
{
    await service.SetupData();
    return Results.Created("/brands", new {Message = "we have setted up zi data"});
});

app.MapGet("/brands", async (ISneakerService service) => Results.Ok(await service.GetBrands()));

app.MapGet("/occasions", async (ISneakerService service) => Results.Ok(await service.GetOccasions()));

app.MapGet("/sneakers", async (ISneakerService service) => Results.Ok(await service.GetSneakers()));

app.MapGet("/sneakers/{sneakerId}",
    async (ISneakerService service, string sneakerId) => Results.Ok(await service.GetSneakerBySneakerId(sneakerId)));

app.MapPost("/sneakers", async (ISneakerService service, IValidator<Sneaker> sneakerValidator, Sneaker sneaker) =>
{
    var validationResult = sneakerValidator.Validate(sneaker);

    if (validationResult.IsValid)
    {
        return Results.Created("/registrations", await service.AddSneaker(sneaker));
    }

    var errors = validationResult.Errors.Select(x => new {errors = x.ErrorMessage});
    return Results.BadRequest(errors);
});

app.Run("http://localhost:3000");
//Hack om testen te doen werken 
// public partial class Program { }