

using RestaurantManagement.Reservation.Api.Features.Tables.Create;
using RestaurantManagement.Reservation.Api.Features.Tables.Dtos;

namespace RestaurantManagement.Reservation.Api.Features.Tables
{
    public class TableMapping: Profile
    {
        public TableMapping()
        {
            CreateMap<CreateTableCommand, Table>();
            CreateMap<Table, TableDto>().ReverseMap();
        }
    }
}
