using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Pages.Order.ViewModel;
using RestaurantManagement.Web.Services;

namespace RestaurantManagement.Web.Pages.Order
{
    [Authorize(Roles = "customer,instructor")]
    public class HistoryModel(OrderService orderService) : BasePageModel
    {
        public List<OrderHistoryViewModel> OrderHistoryList { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var response = await orderService.GetHistory();


            if (response.IsFail) return ErrorPage(response);

            OrderHistoryList = response.Data!;


            return Page();
        }
    }
}
