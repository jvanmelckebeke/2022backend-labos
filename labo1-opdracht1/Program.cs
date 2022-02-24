using labo1_opdracht1.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var wines = new List<Wine>
{
    new()
    {
        WineId = 0,
        Name = "Muller Kogl",
        Year = 2019,
        Country = "Austria",
        Color = "White",
        Price = 12.5M,
        Grapes = "Gruner Veltliner"
    },
    new()
    {
        WineId = 1,
        Name = "Sangrato Barolo",
        Country = "Italy",
        Price = 35,
        Color = "Red",
        Year = 2005,
        Grapes = "Nebiollo"
    }
};

app.MapGet("/", () => "Hello World!");

app.MapGet("/wine", () => Results.Redirect("/wines/0", preserveMethod: true));

app.MapGet("/wines/{wineId}", (int wineId) =>
{
    var wine = wines.Find(w => w.WineId == wineId);

    return wine == null ? Results.NotFound() : Results.Ok(wine);
});

app.MapGet("/wines", () => Results.Ok(wines));

app.MapPost("/wines", (Wine wine) =>
{
    wine.WineId = wines.Count + 1;
    wines.Add(wine);
    return Results.Created($"/wine/{wine.WineId}", wine);
});

app.MapDelete("/wines/{wineId}", (int wineId) =>
{
    var wine = wines.Find(w => w.WineId == wineId);

    if (wine == null)
    {
        return Results.NotFound();
    }

    wines.Remove(wine);
    return Results.Ok();
});

app.MapPut("/wines", (Wine wine) =>
{
    var existingWine = wines.Find(w => w.WineId == wine.WineId);

    if (existingWine == null)
    {
        return Results.NotFound();
    }
    
    existingWine.Name = wine.Name;
    existingWine.Year = wine.Year;

    return Results.Ok(existingWine);
});

app.Run("http://localhost:3000");