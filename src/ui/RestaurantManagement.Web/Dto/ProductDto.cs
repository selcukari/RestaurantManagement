namespace RestaurantManagement.Web.Dto
{
    public record ProductDto
    (
    Guid Id,
    string Name,
    string Description,
    int Quantity,
    decimal Price,
    string ImageUrl,
    DateTime Created,
    MenuDto Menum,
    FeatureDto? Feature = null);
}
