using RestaurantManagement.Basket.Api.Dto;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery: IRequestByServiceResult<BasketDto>;
}
