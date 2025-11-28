using AutoMapper;
using MediatR;
using RestaurantManagement.Order.Application.Contracts.Repositories;
using RestaurantManagement.Order.Application.UseCases.Orders.CreateOrder;
using RestaurantManagement.Shared;
using RestaurantManagement.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Order.Application.UseCases.Orders.GetOrders
{
    public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper)
    : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request,
        CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByBuyerId(identityService.UserId);


            var response = orders.Select(o =>
                new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();


            return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
        }
    }
}
