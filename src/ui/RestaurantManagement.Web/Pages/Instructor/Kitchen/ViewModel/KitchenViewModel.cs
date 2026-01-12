using RestaurantManagement.Web.Pages.Order.ViewModel;
using System.Collections.Immutable;

namespace RestaurantManagement.Web.Pages.Instructor.Kitchen.ViewModel
{
    public record KitchenViewModel(string UserFullName, string Created)
    {
        private List<KitchenItemViewModel> KitchenItems { get; } = [];

        public ImmutableList<KitchenItemViewModel> GetItems => KitchenItems.ToImmutableList();

        public void AddItem(Guid productId, string productName, int Quantity)
        {
            KitchenItems.Add(new KitchenItemViewModel(productId, productName, Quantity));
        }
    }
}
