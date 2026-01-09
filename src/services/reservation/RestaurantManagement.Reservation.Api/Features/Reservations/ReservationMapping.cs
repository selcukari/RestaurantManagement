

using RestaurantManagement.Reservation.Api.Features.Reservations.Create;
using RestaurantManagement.Reservation.Api.Features.Reservations.Dtos;

namespace RestaurantManagement.Reservation.Api.Features.Reservations
{
    public class ReservationMapping: Profile
    {
        public ReservationMapping()
        {
            CreateMap<CreateReservationCommand, Reservation>();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }
    }
}
