using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Order.Domain.Entities;
using RestaurantManagement.Order.Application.Contracts.Refit.PaymentService;
using RestaurantManagement.Order.Application.Contracts.UnitOfWork;
using RestaurantManagement.Order.Application.Contracts.Repositories;
using Microsoft.Extensions.Hosting;

namespace RestaurantManagement.Order.Application.BackgroundServices
{
    public class CheckPaymentStatusOrderBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();

            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            // iptal request gelmedigi surece bu dongu devam edecek
            while (!stoppingToken.IsCancellationRequested)
            {
                var orders = orderRepository.Where(x => x.Status == OrderStatus.WaitingForPayment)
                    .ToList();

                foreach (var order in orders)
                {
                    var paymentStatusResponse = await paymentService.GetStatusAsync(order.Code);

                    if (paymentStatusResponse.IsPaid!)
                    {
                        await orderRepository.SetStatus(order.Code, paymentStatusResponse.PaymentId!.Value,
                            OrderStatus.Paid);
                        await unitOfWork.CommitAsync(stoppingToken);
                    }
                }

                await Task.Delay(5 * 60 * 1000, stoppingToken); // 5 dk bekleme
            }
        }
    }
}
