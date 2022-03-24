using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace labo5_sneakers.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? OrderId { get; set; }
    public string? Email { get; set; }
    public string? SneakerId { get; set; }
    public int NumberOfItems { get; set; }

    public override string ToString()
    {
        return $"{nameof(OrderId)}: {OrderId}, {nameof(Email)}: {Email}, {nameof(SneakerId)}: {SneakerId}, {nameof(NumberOfItems)}: {NumberOfItems}";
    }
}