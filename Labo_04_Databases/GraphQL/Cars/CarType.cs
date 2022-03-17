namespace Backend_Labo_01_Cars.GraphQL.Cars;

public class CarType : ObjectType<Car>
{
    protected override void Configure(IObjectTypeDescriptor<Car> descriptor)
    {
        descriptor.Description("a car");
        descriptor.Field(c => c.Brand).Description("car brand").Type<BrandType>();
        descriptor.Field(car => car.Name).Description("car name");
        descriptor.Name("Car");
    }
}