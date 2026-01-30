namespace RestaurantManagement.Web.Dto
{
    public class KitchenReporDto
    {
        public string UserFullName { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        public List<KitchenReporDetail> KitchenReporDetails { get; set; }
    }

    public class KitchenReporDetail
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
