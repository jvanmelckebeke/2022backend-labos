using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace labo5_sneakers.Models;

public class Occasion
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? OccasionId { get; set; }
    public string? Description { get; set; }

    public override string ToString()
    {
        return $"{nameof(OccasionId)}: {OccasionId}, {nameof(Description)}: {Description}";
    }
}