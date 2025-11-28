using RestaurantManagement.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Order.Application.UseCases.Orders.GetOrders
{
    public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;
}
