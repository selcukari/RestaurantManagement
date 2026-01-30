using RestaurantManagement.Bus.Events;
using RestaurantManagement.Reporting.Api.Features.Reporting;
using RestaurantManagement.Reporting.Api.Repositories;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reporting.Api.Consumers
{
    public class ReportingCreatedKitchenEventConsumer(IServiceProvider serviceProvider)
    : IConsumer<Bus.Events.OrderCreatedForReporingEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedForReporingEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();

            // 1. Mevcut Reporting kaydını bul (Eğer hiç yoksa yeni bir tane oluştur)
            var reporting = await dbContext.Reportings
                .Include(x => x.KitchenRepors) // Listeyi de yüklemesi için Include önemli
                .FirstOrDefaultAsync(); // Şimdilik ilk bulduğunu alıyor

            // eger kayıt yok ise
            if (reporting == null)
            {
                reporting = new RestaurantManagement.Reporting.Api.Features.Reporting.Reporting
                {
                    KitchenRepors = new List<KitchenRepor>()
                };
                dbContext.Reportings.Add(reporting);
            }

            // 2. Yeni rezervasyon objesini oluştur
            var newReservationForKitchenReport = new KitchenRepor
            {
                UserFullName = context.Message.UserFullName,
                Created = context.Message.Created,
                KitchenReporDetails = context.Message.items.Select(item => new KitchenReporDetail
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity
                }).ToList()
            };

            if (reporting.KitchenRepors == null)
            {
                reporting.KitchenRepors = new List<KitchenRepor> { newReservationForKitchenReport };
            }
            else
            {
                // 3. Listenin en başına (0. index) ekle her yeni kayıtı
                reporting.KitchenRepors.Insert(0, newReservationForKitchenReport);
            }
            reporting.Update = DateTime.Now;

            // 4. Değişiklikleri kaydet
            await dbContext.SaveChangesAsync();

            cacheService.Remove("reportings");
        }
    }
}
