using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantManagement.Reservation.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
