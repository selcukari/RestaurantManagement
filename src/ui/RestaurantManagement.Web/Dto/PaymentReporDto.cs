namespace RestaurantManagement.Web.Dto
{
    public class PaymentReporDto
    {
        public DateTime Created { get; set; }
        public decimal TotalPrice { get; set; } = 0;
    }
}
