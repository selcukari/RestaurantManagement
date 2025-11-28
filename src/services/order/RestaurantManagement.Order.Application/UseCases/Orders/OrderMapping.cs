using AutoMapper;
using RestaurantManagement.Order.Application.UseCases.Orders.CreateOrder;
using RestaurantManagement.Order.Domain.Entities;

namespace RestaurantManagement.Order.Application.UseCases.Orders
{
    public class OrderMapping: Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
