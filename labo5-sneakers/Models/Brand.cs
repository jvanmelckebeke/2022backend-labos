using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace labo5_sneakers.Models;

public class Brand
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? BrandId { get; set; }
    public string? Name { get; set; }

    public override string ToString()
    {
        return $"{nameof(BrandId)}: {BrandId}, {nameof(Name)}: {Name}";
    }
}