namespace Backend_Labo_01_Cars.GraphQL.Brands;

public class BrandType : ObjectType<Brand>
{
    protected override void Configure(IObjectTypeDescriptor<Brand> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Description("a car brand");
        descriptor.Field(brand => brand.Country).Description("Country of brand");
        descriptor.Field(brand => brand.Name).Description("Name of brand");
    }
}