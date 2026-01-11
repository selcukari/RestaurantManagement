using RestaurantManagement.Basket.Api.Features.Baskets;

namespace RestaurantManagement.Basket.Api.BackgroundServices
{
    public class DeleteBasketBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();

            var basketService = scope.ServiceProvider.GetRequiredService<BasketService>();

            // iptal request gelmedigi surece bu dongu devam edecek
            while (!stoppingToken.IsCancellationRequested)
            {
                var orders = basketService.DeleteAllBasketsFastAsync();

                await Task.Delay(5 * 60 * 60 * 1000, stoppingToken); // 5 saat bekleme
            }
        }
    }
}
