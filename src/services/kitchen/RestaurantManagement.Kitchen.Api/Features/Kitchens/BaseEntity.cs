using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantManagement.Kitchen.Api.Features.Kitchens
{
    public class BaseEntity
    {
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
