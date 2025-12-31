using RestaurantManagement.Web.Pages.Order.ViewModel;

namespace RestaurantManagement.Web.Pages.Order.Dto
{
    public record GetOrderHistoryResponse(DateTime Created, decimal TotalPrice, List<OrderItemViewModel> Items);
}
