using RestaurantManagement.Shared;

namespace RestaurantManagement.Basket.Api.Features.Baskets.AddBasketItem
{
    public record AddBasketItemCommand(Guid ProductId, string ProductName, decimal ProductPrice, string? ImageUrl, int Quantity)
    : IRequestByServiceResult;
}
