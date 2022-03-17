using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace labo5_sneakers.Models;

public class Sneaker
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? SneakerId { get; set; }

    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Brand? Brand { get; set; }
    public List<Occasion>? Occasions { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(SneakerId)}: {SneakerId}, {nameof(Name)}: {Name}, {nameof(Price)}: {Price}, {nameof(Stock)}: {Stock}, {nameof(Brand)}: {Brand}, {nameof(Occasions)}: {Occasions}";
    }
}