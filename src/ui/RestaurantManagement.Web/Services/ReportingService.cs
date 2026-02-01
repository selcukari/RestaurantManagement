using RestaurantManagement.Web.Dto;
using RestaurantManagement.Web.Services.Refit;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Services
{
    public class ReportingService(
    IReportingRefitService reportingRefitService,
    UserService userService,
    ILogger<ReportingService> logger)
    {
        public async Task<ServiceResult<ReportingViewModel>> GetAllReportingAsync()
        {
            var response = await reportingRefitService.GetAllReportings();

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(new EventId(), null, response.Error);

                return ServiceResult<ReportingViewModel>.Error(
                    "An error occurred while getting the Reporting");
            }

            var dto = response.Content;
            var rawReservations = dto.ReservationRepors ?? new List<ReservationReporDto>();
            var rawKitchens = dto.KitchenRepors ?? new List<KitchenReporDto>();
            var rawPayments = dto.PaymentRepors ?? new List<PaymentReporDto>();

            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-5);

            // 1. En çok rezervasyon yapan benzersiz 5 müşteri son 10 ay
            var topCustomers = rawReservations
                .Where(r => r.ReservationDate >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-10))
                .GroupBy(r => r.CustomerId)
                .Select(g => new ReservationForCustomerViewModel(
                    g.Key,
                    g.First().CustomerFullName, // İlk bulduğu ismi alır
                    g.Count()
                ))
                .OrderByDescending(c => c.Count)
                .Take(5)
                .ToList();

            var lastFiveMonths = rawReservations
                .Where(r => r.ReservationDate >= startDate) // Son 5 ayın başlangıcından itibaren al
                .GroupBy(r => new { r.ReservationDate.Year, r.ReservationDate.Month })
                .Select(g => new {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Date) // En yeni ayı en başa al
                .Take(5) // En yeni 5 ayı seç
                .Select(x => new ReservationForPreferenceViewModel(
                    x.Date.ToString("MMMM yyyy"), // Artık güvenle string'e çevirebiliriz
                    x.Count
                ))
                .ToList();

            // 1. En çok ürün yapan benzersiz 5 ay
            var topKitchenForProducts = rawKitchens
                .Where(r => r.Created >= startDate) // Son 5 ay filtresi
                .SelectMany(r => r.KitchenReporDetails) // List<KitchenReporDetail> içindeki tüm ürünleri tek bir listeye indirger
                .GroupBy(p => p.Id) // Ürün adına göre grupla (veya Id'ye göre)
                .Select(g => new KitchenForProductViewModel(
                    g.First().Name, // Ürün adı
                    g.Sum(x => x.Quantity) // O gruba ait tüm miktarları topla
                ))
                .OrderByDescending(p => p.Count) // En çok satılanı başa al
                .Take(5) // İstersen en çok satılan ilk 5 ürünü alabilirsin
                .ToList();

            // 1. son 5 ay payment
            var topPayments = rawPayments
                .Where(r => r.Created >= startDate)
                .GroupBy(r => new { r.Created.Year, r.Created.Month })
                .Select(g => new PaymentForReportViewModel(
                    g.First().Created.ToString("MMMM yyyy")
                    g.Sum(x => x.TotalPrice)
                ))
                .OrderByDescending(c => c.Created)
                .Take(5)
                .ToList();

            var reportings = new ReportingViewModel(
                dto.Id,
                dto.Update.ToString("dd.MM.yyyy HH:mm"),
                topCustomers,
                lastFiveMonths,
                topKitchenForProducts,
                topPayments
            );

            return ServiceResult<ReportingViewModel>.Success(reportings);
        }
    }
}
