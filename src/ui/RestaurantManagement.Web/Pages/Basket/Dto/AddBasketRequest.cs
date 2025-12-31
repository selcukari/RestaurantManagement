namespace RestaurantManagement.Web.Pages.Basket.Dto
{
    public record AddBasketRequest(
    Guid ProductId,
    string ProductName,
    decimal ProductPrice,
    string? ImageUrl,
    int Quantity
    );
}
