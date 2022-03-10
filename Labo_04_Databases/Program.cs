using Backend_Labo_01_Cars.Configuration;

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");


builder.Services.Configure<DatabaseSettings>(mongoSettings);

var app = builder.Build();
app.MapGet("/helloworld", () => "Hello World");


//GraphQL.SchemaExtensions.RegisterTypeMapping<Brand,BrandType>();
app.Run("http://localhost:5212");