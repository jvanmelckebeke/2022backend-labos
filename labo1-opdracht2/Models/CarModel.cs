namespace labo1_opdracht2.Models;

public class CarModel
{
    public int CarModelId { get; set; }
    public string? Name { get; set; }
    public Brand? Brand { get; set; }

    public int BrandId { get; set; }
}