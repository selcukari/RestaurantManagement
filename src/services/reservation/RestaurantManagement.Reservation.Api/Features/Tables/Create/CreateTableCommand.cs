namespace RestaurantManagement.Reservation.Api.Features.Tables.Create
{
    public record CreateTableCommand : IRequestByServiceResult<Guid>
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public Location Location { get; set; }
        public bool IsAvailable { get; set; } = true;
        public TableStatus Status { get; set; } = 0;
    }
}
