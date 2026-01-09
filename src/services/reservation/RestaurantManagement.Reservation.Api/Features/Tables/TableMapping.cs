

using RestaurantManagement.Reservation.Api.Features.Tables.Create;
using RestaurantManagement.Reservation.Api.Features.Tables.Dtos;

namespace RestaurantManagement.Reservation.Api.Features.Tables
{
    public class ReservationMapping: Profile
    {
        public ReservationMapping()
        {
            CreateMap<CreateTableCommand, Table>();
            CreateMap<Table, TableDto>().ReverseMap();
        }
    }
}
