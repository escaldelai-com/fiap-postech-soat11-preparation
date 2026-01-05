using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Preparation.Data.Model;

public class OrderItemData
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Nome { get; set; }

    public string? Tipo { get; set; }

}
