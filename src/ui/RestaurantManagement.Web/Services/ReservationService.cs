using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.Services.Refit;
using RestaurantManagement.Web.ViewModel;
using System.Text.Json;

namespace RestaurantManagement.Web.Services
{
    public class ReservationService(
    IReservationRefitService reservationRefitService,
    UserService userService,
    ILogger<MenuService> logger)
    {
        public async Task<ServiceResult<List<ReservationViewModel>>> GetAllReservationsAsync()
        {
            var reservationAsResult = await reservationRefitService.GetAllReservations();

            if (!reservationAsResult.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(reservationAsResult.Error.Content!);
                logger.LogError("Error occurred while fetching products");
                //logger.LogProblemDetails(productAsResult.Error);

                return ServiceResult<List<ReservationViewModel>>.Error(
                    "Failed to retrieve product data. Please try again later.");
            }


            var reservations = reservationAsResult.Content!;

            var reservationsViewModel = reservations.Select(c =>
                new ReservationViewModel(
                    c.Id,
                    c.CustomerId.ToString(), // bura da ıd ile fullname bull
                    c.Created.ToLongDateString(),
                    c.ReservationDate.ToLongDateString(),
                    c.StartTime.ToString(),
                    c.EndTime.ToString(),
                    c.GuestCount,
                    c.Table.Id,
                    c.Table.TableNumber
                    )).ToList();

            return ServiceResult<List<ReservationViewModel>>.Success(reservationsViewModel);
        }

    }
}
