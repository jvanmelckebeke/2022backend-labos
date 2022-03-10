
namespace Backend_Labo_01_Cars.Models;

public class Car
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public Brand? Brand { get; set; }
    
    public DateTime? CreatedOn { get; set; }
}