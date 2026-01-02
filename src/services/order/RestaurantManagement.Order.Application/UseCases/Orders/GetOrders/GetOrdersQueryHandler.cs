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
    public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper, ICacheService cacheService)
    : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request,
        CancellationToken cancellationToken)
        {
            var cacheKey = $"orders";

            var response = cacheService.Get<List<GetOrdersResponse>>(cacheKey);

            if (response == null || response.Count == 1)
            {
                var orders = await orderRepository.GetOrderByBuyerId(identityService.UserId);


                response = orders.Select(o =>
                    new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();
                
                cacheService.Set(cacheKey, response, TimeSpan.FromDays(5));
            }

            return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
        }
    }
}
