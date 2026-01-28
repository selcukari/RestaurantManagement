


using RestaurantManagement.Reporting.Api.Features.Reporting.Dtos;

namespace RestaurantManagement.Reporting.Api.Features.Reporting
{
    public class ReportingMapping: Profile
    {
        public ReportingMapping()
        {
            CreateMap<Reporting, ReportingDto>().ReverseMap();
            CreateMap<ReservationRepor, ReservationReporDto>().ReverseMap();
        }
    }
}
