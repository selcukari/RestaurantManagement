using RestaurantManagement.Basket.Api.Data;
using RestaurantManagement.Shared.Services;
using RestaurantManagement.Shared;
using System.Text.Json;
using MediatR;

namespace RestaurantManagement.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(
    IIdentityService identityService,
    BasketService basketService)
    : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(request.ProductId, request.ProductName, request.ImageUrl,
            request.ProductPrice, null, request.Quantity);


            if (string.IsNullOrEmpty(basketAsJson))
            {

                currentBasket = new Data.Basket(identityService.UserId, [newBasketItem]);
                await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);


            var existingBasketItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.ProductId);


            if (existingBasketItem is not null) // basket  var ise sil
                 // TODO : business rule
            {
                currentBasket.Items.Remove(existingBasketItem);
                newBasketItem.Quantity += existingBasketItem.Quantity;
            }


            currentBasket.Items.Add(newBasketItem);


            currentBasket.ApplyAvailableDiscount();


            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
