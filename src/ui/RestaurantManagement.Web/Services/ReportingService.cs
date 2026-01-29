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

            // 1. En çok rezervasyon yapan benzersiz 5 müşteri
            var topCustomers = rawReservations
                .GroupBy(r => r.CustomerId)
                .Select(g => new ReservationForCustomerViewModel(
                    g.Key,
                    g.First().CustomerFullName, // İlk bulduğu ismi alır
                    g.Count()
                ))
                .OrderByDescending(c => c.Count)
                .Take(5)
                .ToList();

            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-5);

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

            var reportings = new ReportingViewModel(
                dto.Id,
                dto.Update.ToString("dd.MM.yyyy HH:mm"),
                topCustomers,
                lastFiveMonths
            );

            return ServiceResult<ReportingViewModel>.Success(reportings);
        }
    }
}
