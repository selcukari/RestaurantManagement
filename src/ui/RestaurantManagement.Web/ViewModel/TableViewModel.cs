namespace RestaurantManagement.Web.ViewModel
{
    public record TableViewModel(Guid Id, int TableNumber, string UserFullName, int Capacity, DateTime Created, string Location,
    string Status)
    {
        // Dropdown'da görünecek format: "2 - Kapasite: 4 - Konum: Pencere"
        public string DisplayText => $"{TableNumber} - Kapasite: {Capacity} - Konum: {Location}";
    }
}
