using FluentValidation;
using FluentValidation.AspNetCore;
using labo5_sneakers.Configuration;
using labo5_sneakers.Context;
using labo5_sneakers.MiddleWare;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;
using labo5_sneakers.Services;

var builder = WebApplication.CreateBuilder(args);

var dbsettings = builder.Configuration.GetSection("MongoConnection");
var apikeySettings = builder.Configuration.GetSection("ApiKeySettings");

builder.Services.Configure<DatabaseSettings>(dbsettings);
builder.Services.Configure<ApiKeySettings>(apikeySettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();

builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ISneakerRepository, SneakerRepository>();
builder.Services.AddTransient<IOccasionRepository, OccasionRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddTransient<ISneakerService, SneakerService>();
builder.Services.AddTransient<IStockNotificationService, StockNotificationService>();

builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<SneakerRepository>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseMiddleware<ApiKeyMiddleware>();

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
    async (ISneakerService service, string sneakerId) =>
    {
        var sneaker = await service.GetSneakerBySneakerId(sneakerId);

        return sneaker != null
            ? Results.Ok(await service.GetSneakerBySneakerId(sneakerId))
            : Results.NotFound(sneakerId);
    });

app.MapPost("/sneakers", async (ISneakerService service, IValidator<Sneaker> sneakerValidator, Sneaker sneaker) =>
{
    var validationResult = sneakerValidator.Validate(sneaker);

    if (validationResult.IsValid)
    {
        var created = await service.AddSneaker(sneaker);
        return Results.Created($"/sneaker/{created.SneakerId}", created);
    }

    var errors = validationResult.Errors.Select(x => new {errors = x.ErrorMessage});
    return Results.BadRequest(errors);
});

app.MapPost("/orders",
    async (ISneakerService service, Order order) =>
    {
        var orderDone = await service.AddOrder(order);

        return orderDone == null
            ? Results.NotFound(order.SneakerId)
            : Results.Ok(orderDone);
    });

app.Run("http://localhost:3000");


//Hack om testen te doen werken 
// app.Run();
//
// public partial class Program
// {
// }