
namespace RestaurantManagement.Web.Pages.Kitchen.ViewModel
{
    public record KitchenPageViewModel
    {
        public List<KitchenViewModelItem> Items { get; set; } = [];
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }

    }
    public record KitchenViewModelItem(
      Guid Id,
      string Name,
      int Quantity
     );
}

