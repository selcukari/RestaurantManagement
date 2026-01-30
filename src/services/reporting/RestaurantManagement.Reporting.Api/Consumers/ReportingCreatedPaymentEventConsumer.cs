using RestaurantManagement.Bus.Events;
using RestaurantManagement.Reporting.Api.Features.Reporting;
using RestaurantManagement.Reporting.Api.Repositories;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reporting.Api.Consumers
{
    public class ReportingCreatedPaymentEventConsumer(IServiceProvider serviceProvider)
    : IConsumer<Bus.Events.ReportingForPaymentEvent>
    {
        public async Task Consume(ConsumeContext<ReportingForPaymentEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();

            // 1. Mevcut Reporting kaydını bul (Eğer hiç yoksa yeni bir tane oluştur)
            var reporting = await dbContext.Reportings
                .Include(x => x.PaymentRepors) // Listeyi de yüklemesi için Include önemli
                .FirstOrDefaultAsync(); // Şimdilik ilk bulduğunu alıyor

            // eger kayıt yok ise
            if (reporting == null)
            {
                reporting = new RestaurantManagement.Reporting.Api.Features.Reporting.Reporting
                {
                    PaymentRepors = new List<PaymentRepor>()
                };
                dbContext.Reportings.Add(reporting);
            }

            // 2. Yeni newPaymentReport objesini oluştur
            var newPaymentReport = new PaymentRepor
            {
                Created = context.Message.Created,
                TotalPrice = context.Message.TotalPrice,
            };

            // 3. Listenin en başına (0. index) ekle her yeni kayıtı
            if (reporting.PaymentRepors == null)
            {
                reporting.PaymentRepors = new List<PaymentRepor> { newPaymentReport };
            }
            else
            {
                reporting.PaymentRepors.Insert(0, newPaymentReport);
            }
            reporting.Update = DateTime.Now;

            cacheService.Remove("reportings");

            // 4. Değişiklikleri kaydet
            await dbContext.SaveChangesAsync();
        }
    }
}
