using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantManagement.Reporting.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
