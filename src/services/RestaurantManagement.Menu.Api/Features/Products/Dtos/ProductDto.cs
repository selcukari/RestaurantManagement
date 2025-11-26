namespace RestaurantManagement.Menu.Api.Features.Products.Dtos
{
    public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    DateTime Created,
    MenuDto Menum,
    FeatureDto? Feature = null);
}
