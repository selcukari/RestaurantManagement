namespace RestaurantManagement.Web.ViewModel
{
    public record TableViewModel(Guid Id, int TableNumber, string UserFullName, int Capacity, DateTime Created, string Location,
    string Status);
}
