namespace RestaurantManagement.Bus.Events
{
   public record ReportingCreatedReservationEvent(Guid Id, string CustomerFullName, DateTime ReservationDate, TimeSpan StartTime, TimeSpan EndTime,
       int GuestCount, Guid CustomerId, Guid TableId);
   
}
