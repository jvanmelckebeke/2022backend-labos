using Backend_Labo_01_Cars.Configuration;
using Backend_Labo_01_Cars.Repositories;
using Backend_Labo_01_Cars.Services;
using DotNetCore.MongoDB;
using MongoContext = Backend_Labo_01_Cars.DataContext.MongoContext;

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");


builder.Services.Configure<DatabaseSettings>(mongoSettings);

builder.Services.AddTransient<MongoContext, MongoContext>();

builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ICarRepository, CarRepository>();

builder.Services.AddTransient<ICarService, ICarService>();

var app = builder.Build();
app.MapGet("/helloworld", () => "Hello World");


//GraphQL.SchemaExtensions.RegisterTypeMapping<Brand,BrandType>();
app.Run("http://localhost:5212");