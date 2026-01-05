using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Restaurant.Preparation.Application.DTO;

namespace Restaurant.Preparation.Data.Model;

public class OrderData
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public DateTime? Data { get; set; }

    public int? Numero { get; set; }

    public string? Cliente { get; set; }

    public string? Status { get; set; }

    public OrderItemDto[] Items { get; set; } = [];

}
