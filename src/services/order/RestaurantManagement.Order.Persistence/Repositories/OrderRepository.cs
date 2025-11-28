using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Order.Application.Contracts.Repositories;
using RestaurantManagement.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context)
    : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {
        public Task<List<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId)
        {
            return context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == buyerId)
                .OrderByDescending(x => x.Created).ToListAsync();
        }

        public async Task SetStatus(string orderCode, Guid paymentId, OrderStatus status)
        {
            var order = await context.Orders.FirstAsync(x => x.Code == orderCode);

            order.Status = status;
            order.PaymentId = paymentId;
            context.Update(order);
        }
    }
}
