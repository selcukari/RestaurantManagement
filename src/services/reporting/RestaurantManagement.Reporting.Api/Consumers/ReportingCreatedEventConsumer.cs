using RestaurantManagement.Bus.Events;
using RestaurantManagement.Reporting.Api.Features.Reporting;
using RestaurantManagement.Reporting.Api.Repositories;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reporting.Api.Consumers
{
    public class ReportingCreatedEventConsumer(IServiceProvider serviceProvider, ICacheService cacheService)
    : IConsumer<Bus.Events.ReportingCreatedEvent>
    {
        public async Task Consume(ConsumeContext<ReportingCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // 1. Mevcut Reporting kaydını bul (Eğer hiç yoksa yeni bir tane oluştur)
            var reporting = await dbContext.Reportings
                .Include(x => x.ReservationRepors) // Listeyi de yüklemesi için Include önemli
                .FirstOrDefaultAsync(); // Şimdilik ilk bulduğunu alıyor

            // eger kayıt yok ise
            if (reporting == null)
            {
                reporting = new RestaurantManagement.Reporting.Api.Features.Reporting.Reporting
                {
                    Update = DateTime.Now,
                    ReservationRepors = new List<ReservationRepor>()
                };
                dbContext.Reportings.Add(reporting);
            }

            // 2. Yeni rezervasyon objesini oluştur
            var newReservationReport = new ReservationRepor
            {
                Id = context.Message.Id,
                CustomerFullName = context.Message.CustomerFullName,
                TableId = context.Message.TableId,
                CustomerId = context.Message.CustomerId,
                ReservationDate = context.Message.ReservationDate,
                EndTime = context.Message.EndTime,
                StartTime = context.Message.StartTime,
                GuestCount = context.Message.GuestCount,
            };

            // 3. Listenin en başına (0. index) ekle her yeni kayıtı
            reporting.ReservationRepors.Insert(0, newReservationReport);
            reporting.Update = DateTime.Now;

            // 4. Değişiklikleri kaydet
            await dbContext.SaveChangesAsync();
        }
    }
}
