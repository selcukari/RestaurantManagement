namespace RestaurantManagement.Web.Dto
{
    public record UpdateProductRequest(
     Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    string? ImageUrl,
    Guid MenumId);
}
