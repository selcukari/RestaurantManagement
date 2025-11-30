using RestaurantManagement.Shared;

namespace RestaurantManagement.Order.Application.UseCases.Orders.GetOrders
{
    public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;
}
