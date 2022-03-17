using System.Diagnostics;
using System.Globalization;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using labo2_opdracht1.Configuration;
using labo2_opdracht1.DTO;
using labo2_opdracht1.Models;
using labo2_opdracht1.Repositories;
using labo2_opdracht1.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var csvSettings = builder.Configuration.GetSection("CsvConfig");

builder.Services.Configure<CsvConfig>(csvSettings);

builder.Services.AddTransient<IVaccinationLocationRepository, CsvVaccinationLocationRepository>();
builder.Services.AddTransient<IVaccineTypeRepository, CsvVaccineTypeRepository>();
builder.Services.AddTransient<IVaccineRegistrationRepository, CsvVaccineRegistrationRepository>();
builder.Services.AddTransient<IVaccinationService, VaccinationService>();

builder.Services.AddTransient<ICsvRepository<VaccineType>, CsvRepository<VaccineType>>();
builder.Services.AddTransient<ICsvRepository<VaccineRegistration>, CsvRepository<VaccineRegistration>>();
builder.Services.AddTransient<ICsvRepository<VaccinationLocation>, CsvRepository<VaccinationLocation>>();

builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<VaccineRegistrationRepository>());
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerFeature>()
        ?.Error;
    if (exception is not null)
    {
        Console.WriteLine("exception occured");
        Console.Error.WriteLine(exception);

        var response = new {error = exception.Message};
        context.Response.StatusCode = 500;

        await context.Response.WriteAsJsonAsync(response);
    }
}));

app.MapGet("/locations",
    (IVaccinationService vaccinationService) => { return Results.Ok(vaccinationService.GetLocations()); });

app.MapGet("/vaccines",
    (IVaccinationService vaccinationService) => { return Results.Ok(vaccinationService.GetVaccines()); });

app.MapGet("/registrations",
    (IMapper mapper, IVaccinationService vaccinationService) =>
    {
        var mapped = mapper.Map<List<VaccineRegistrationDTO>>(vaccinationService.GetRegistrations());
        return Results.Ok(mapped);
    });

app.MapPost("/registrations",
    (IVaccinationService vaccinationService, IValidator<VaccineRegistration> registrationValidator,
        VaccineRegistration vaccineRegistration) =>
    {
        var validationResult = registrationValidator.Validate(vaccineRegistration);

        if (validationResult.IsValid)
        {
            return Results.Created("/registrations", vaccinationService.AddRegistration(vaccineRegistration));
        }
        else
        {
            var errors = validationResult.Errors.Select(x => new {errors = x.ErrorMessage});
            return Results.BadRequest(errors);
        }
    });

app.Run("http://localhost:3000");