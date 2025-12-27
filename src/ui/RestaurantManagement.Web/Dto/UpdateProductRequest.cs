namespace RestaurantManagement.Web.Dto
{
    public record UpdateProductRequest(
     Guid Id,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl,
    Guid MenumId);
}
