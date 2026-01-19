

using RestaurantManagement.Reservation.Api.Features.Tables.Create;
using RestaurantManagement.Reservation.Api.Features.Tables.Dtos;
using RestaurantManagement.Reservation.Api.Features.Tables.Update;

namespace RestaurantManagement.Reservation.Api.Features.Tables
{
    public class ReservationMapping: Profile
    {
        public ReservationMapping()
        {
            CreateMap<CreateTableCommand, Table>().ReverseMap();
            CreateMap<Table, TableDto>().ReverseMap();
            CreateMap<UpdateTableCommand, Table>().ReverseMap();
        }
    }
}
