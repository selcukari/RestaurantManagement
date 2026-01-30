namespace RestaurantManagement.Bus.Events
{
   public record ReportingForPaymentEvent(DateTime Created, decimal TotalPrice);
}
