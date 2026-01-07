using RestaurantManagement.Web.Pages.Kitchen.ViewModel;

namespace RestaurantManagement.Web.Pages.Kitchen.Dto
{
    public record KitchenDto(Guid UserId, DateTime Created, List<KitchenItemViewModel> Items);
}
