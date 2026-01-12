
namespace RestaurantManagement.Web.Pages.Instructor.Kitchen.ViewModel
{
    public record KitchenPageViewModel
    {
        public List<KitchenViewModelItem> Items { get; set; } = [];
        public string UserFullName { get; set; } = string.Empty;
        public DateTime Created { get; set; }

    }
    public record KitchenViewModelItem(
      Guid Id,
      string Name,
      int Quantity
     );
}

