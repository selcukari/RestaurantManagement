using RestaurantManagement.Web.Dto;
using RestaurantManagement.Web.Services.Refit;
using RestaurantManagement.Web.ViewModel;
using System.Text.Json;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace RestaurantManagement.Web.Services
{
    public class ReservationService(
    IReservationRefitService reservationRefitService,
    ILogger<MenuService> logger)
    {
        public async Task<ServiceResult<List<ReservationViewModel>>> GetAllReservationsAsync()
        {
            var reservationAsResult = await reservationRefitService.GetAllReservations();

            if (!reservationAsResult.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(reservationAsResult.Error.Content!);
                logger.LogError("Error occurred while fetching reservations");
                //logger.LogProblemDetails(productAsResult.Error);

                return ServiceResult<List<ReservationViewModel>>.Error(
                    "Failed to retrieve reservation data. Please try again later.");
            }


            var reservations = reservationAsResult.Content!;

            var reservationsViewModel = reservations.Select(c =>
                new ReservationViewModel(
                    c.Id,
                    c.CustomerFullName,
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

        public async Task<ServiceResult<List<TableViewModel>>> GetTablesAsync()
        {
            var response = await reservationRefitService.GetTablesAsync();
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while fetching tables");
                return ServiceResult<List<TableViewModel>>.Error("Fail to retrieve table. Please try again later");
            }

            var tables = response!.Content!
                .Select(c => new TableViewModel(c.Id, c.TableNumber, c.UserFullName, c.Capacity, c.Created, c.Location.ToString(), c.Status.ToString()))
                .ToList();
            return ServiceResult<List<TableViewModel>>.Success(tables);
        }

        public async Task<ServiceResult> CreateTableAsync(CreateTableViewModel model)
        {
            var request = new AddTableRequest(
                model.TableNumber,
                model.Capacity,
                model.Location
            );

            var response = await reservationRefitService.AddTableItemAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while creating table");
                return ServiceResult.Error("Fail to create table. Please try again later");
            }

            return ServiceResult.Success();
        }
        public async Task<ServiceResult> CreateReservationAsync(CreateReservationViewModel model)
        {
            var request = new AddReservationRequest(
                model.TableId,
                model.ReservationDate,
                model.StartTime,
                model.EndTime,
                model.GuestCount
            );

            var response = await reservationRefitService.AddReservationItemAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while creating Reservation");
                return ServiceResult.Error("Fail to create table. Please try again later");
            }

            return ServiceResult.Success();
        }
        public async Task<ServiceResult> DeleteTableAsync(Guid Id)
        {
            var response = await reservationRefitService.DeleteTableAsync(Id);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while creating Reservation");
                return ServiceResult.Error("Fail to create table. Please try again later");
            }

            return ServiceResult.Success();
        }
        public async Task<ServiceResult> DeleteReservationAsync(Guid Id)
        {
            var response = await reservationRefitService.DeleteReservationAsync(Id);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while creating Reservation");
                return ServiceResult.Error("Fail to create table. Please try again later");
            }

            return ServiceResult.Success();
        }
    }
}
