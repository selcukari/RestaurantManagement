namespace RestaurantManagement.Reporting.Api.Features.Reporting.Dtos
{
    public class KitchenReporDto
    {
        public string UserFullName { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        public List<KitchenReporDetail> KitchenReporDetails { get; set; }
    }
}