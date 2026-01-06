using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Pages.Basket.Dto;
using RestaurantManagement.Web.Pages.Basket.ViewModel;
using RestaurantManagement.Web.Services;

namespace RestaurantManagement.Web.Pages.Basket
{
    [Authorize]
    public class IndexModel(MenuService menuService, BasketService basketService) : BasePageModel
    {
        public BasketPageViewModel Basket { get; set; } = new();
        

        public async Task<IActionResult> OnGet()
        {
            var basketAsResult = await basketService.GetBasketPageViewModelAsync();

            if (basketAsResult.IsFail)
                return ErrorPage(basketAsResult, "Index");
            Basket = basketAsResult.Data!;

            return Page();
        }
        public async Task<IActionResult> OnGetAddBasketAsync(Guid productId, int quantity)
        {
            var product = await menuService.GetProduct(productId);


            var createOrUpdateBasket = new AddBasketRequest(product.Data!.Id, product.Data.Name,
                product.Data.Price, product.Data.ImageUrl, quantity);


            var result = await basketService.CreateOrUpdateBasketAsync(createOrUpdateBasket);

            return result.IsFail ? ErrorPage(result, "Index") : SuccessPage("product added to basket", "Index");
        }

        public async Task<IActionResult> OnGetDeleteAsync(Guid productId)
        {
            var result = await basketService.DeleteBasketAsync(productId);

            return result.IsFail ? ErrorPage(result, "Index") : SuccessPage("product deleted from basket", "Index");
        }

        public async Task<IActionResult> OnPostApplyDiscountAsync(string couponCode)
        {
            var response = await basketService.ApplyDiscountAsync(couponCode);

            return response.IsFail ? ErrorPage(response, "Index") : SuccessPage("discount coupon applied", "Index");
        }

        public async Task<IActionResult> OnGetRemoveDiscountAsync()
        {
            var response = await basketService.RemoveDiscountAsync();

            return response.IsFail ? ErrorPage(response, "Index") : SuccessPage("discount coupon removed", "Index");
        }
    }
}
