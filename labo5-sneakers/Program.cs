using FluentValidation;
using FluentValidation.AspNetCore;
using labo5_sneakers.Configuration;
using labo5_sneakers.Context;
using labo5_sneakers.Models;
using labo5_sneakers.Repositories;
using labo5_sneakers.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var dbsettings = builder.Configuration.GetSection("MongoConnection");
var authenticationSettings = builder.Configuration.GetSection("AuthenticationSettings");

builder.Services.Configure<DatabaseSettings>(dbsettings);
builder.Services.Configure<AuthenticationSettings>(authenticationSettings);


builder.Services.AddTransient<IMongoContext, MongoContext>();

builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ISneakerRepository, SneakerRepository>();
builder.Services.AddTransient<IOccasionRepository, OccasionRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddTransient<ISneakerService, SneakerService>();
builder.Services.AddTransient<IStockNotificationService, StockNotificationService>();

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<SneakerRepository>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddAuthentication();

builder.Services.AddAuthorization(options =>{});

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options=>
    {
        options.TokenValidationParameters = new(){
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
            ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretForKey"]))
        };
    }
);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapSwagger();
app.UseSwaggerUI();


app.MapGet("/", () => "Hello World!");

app.MapGet("/setup", [Authorize] async (ISneakerService service) =>
{
    await service.SetupData();
    return Results.Created("/brands", new {Message = "we have setted up zi data"});
});

app.MapGet("/brands", [Authorize] async (ISneakerService service) => Results.Ok(await service.GetBrands()));

app.MapGet("/occasions", [Authorize] async (ISneakerService service) => Results.Ok(await service.GetOccasions()));

app.MapGet("/sneakers", [Authorize] async (ISneakerService service) => Results.Ok(await service.GetSneakers()));

app.MapGet("/sneakers/{sneakerId}",
    [Authorize] async (ISneakerService service, string sneakerId) =>
    {
        var sneaker = await service.GetSneakerBySneakerId(sneakerId);

        return sneaker != null
            ? Results.Ok(await service.GetSneakerBySneakerId(sneakerId))
            : Results.NotFound(sneakerId);
    });

app.MapPost("/sneakers", [Authorize]
    async (ISneakerService service, IValidator<Sneaker> sneakerValidator, Sneaker sneaker) =>
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
    [Authorize] async (ISneakerService service, Order order) =>
    {
        var orderDone = await service.AddOrder(order);

        return orderDone == null
            ? Results.NotFound(order.SneakerId)
            : Results.Ok(orderDone);
    });

app.MapPost("/authenticate",
    (IAuthenticationService service, AuthenticationRequestBody auth) =>
    {
        var resp = service.Authenticate(auth);
        if (resp is null)
        {
            return Results.Unauthorized();
        }
        else
        {
            return Results.Ok(resp);
        }
    });

app.Run("http://localhost:3000");


//Hack om testen te doen werken 
// app.Run();
//
// public partial class Program
// {
// }