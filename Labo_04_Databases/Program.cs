var builder = WebApplication.CreateBuilder(args);

 



var app = builder.Build();
app.MapGet("/helloworld",() => "Hello World");




//GraphQL.SchemaExtensions.RegisterTypeMapping<Brand,BrandType>();
app.Run("http://localhost:3000");
