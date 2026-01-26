using RestaurantManagement.Web.Dto;
using RestaurantManagement.Web.Services.Refit;
using RestaurantManagement.Web.ViewModel;
using System.Text.Json;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace RestaurantManagement.Web.Services
{
    public class ReservationService(
    IReservationRefitService reservationRefitService, UserService userService,
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

                return ServiceResult<List<ReservationViewModel>>.Error(problemDetails.Title ??
                    "Failed to retrieve reservation data.", problemDetails.Detail!);
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
        public async Task<ServiceResult<List<ReservationViewModel>>> GetAllByIdReservationsAsync()
        {
            var reservationAsResult = await reservationRefitService.GetAllByIdReservations(userService.UserId);

            if (!reservationAsResult.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(reservationAsResult.Error.Content!);
                logger.LogError("Error occurred while fetching reservations");
                //logger.LogProblemDetails(productAsResult.Error);

                return ServiceResult<List<ReservationViewModel>>.Error(
                    problemDetails.Title ?? "Failed to retrieve reservation data.", problemDetails.Detail!);
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
        public async Task<ServiceResult<ReservationViewModel>> GetReservationAsync(Guid reservationId)
        {
            var response = await reservationRefitService.GetReservation(reservationId);

            if (!response.IsSuccessStatusCode)
                return ServiceResult<ReservationViewModel>.FailFromProblemDetails(response.Error);


            var reservation = response.Content!;
            var reservationViewModel = new ReservationViewModel(reservation.Id, reservation.CustomerFullName, reservation.Created.ToLongDateString(),
                  reservation.ReservationDate.ToLongDateString(), reservation.StartTime.ToString(), reservation.EndTime.ToString(), reservation.GuestCount, reservation.Table.Id, reservation.Table.TableNumber);

            return ServiceResult<ReservationViewModel>.Success(reservationViewModel);
        }

        public async Task<ServiceResult<List<TableViewModel>>> GetTablesAsync()
        {
            var response = await reservationRefitService.GetTablesAsync();
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while fetching tables");
                return ServiceResult<List<TableViewModel>>.Error(problemDetails.Title ?? "Fail to getAll table.", problemDetails.Detail!);
            }

            var tables = response!.Content!
                .Select(c => new TableViewModel(c.Id, c.TableNumber, c.UserFullName, c.Capacity, c.Created, c.Location.ToString(), c.Status.ToString(), c.IsAvailable))
                .ToList();
            return ServiceResult<List<TableViewModel>>.Success(tables);
        }
        public async Task<ServiceResult<TableViewModel>> GetTableAsync(Guid tableId)
        {
            var response = await reservationRefitService.GetTableAsync(tableId);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while fetching tables");
                return ServiceResult<TableViewModel>.Error(problemDetails.Title ?? "Fail to get table.", problemDetails.Detail!);
            }

            var table = response!.Content!;
            var tableViewModel = new TableViewModel(table.Id, table.TableNumber, table.UserFullName, table.Capacity, table.Created, table.Location.ToString(), table.Status.ToString(), table.IsAvailable);
            
            return ServiceResult<TableViewModel>.Success(tableViewModel);
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
                return ServiceResult.Error(problemDetails.Title ?? "Fail to create table.", problemDetails.Detail!);
            }

            return ServiceResult.Success();
        }
        public async Task<ServiceResult> UpdateTableAsync(UpdateTableViewModel model)
        {

            var response = await reservationRefitService.UpdateTableAsync(
                new UpdateTableRequest(
                    model.Id,
                    model.TableNumber, model.Capacity, model.Location, model.IsAvailable)
            );

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while updateing table");
                return ServiceResult.Error(problemDetails.Title ?? "Fail to update table.", problemDetails.Detail!);
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
                return ServiceResult.Error(problemDetails.Title ?? "Fail to create reservation.", problemDetails.Detail!);
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
                return ServiceResult.Error(problemDetails.Title ?? "Fail to create table.", problemDetails.Detail!);
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
                return ServiceResult.Error(problemDetails.Title ?? "Fail to delete reservation.", problemDetails.Detail!);
            }

            return ServiceResult.Success();
        }
    }
}
