namespace RestaurantManagement.Web.Dto
{
    public record UpdateTableRequest(
     Guid Id,
    int TableNumber,
    int Capacity,
    string Location, bool IsAvailable);
}
