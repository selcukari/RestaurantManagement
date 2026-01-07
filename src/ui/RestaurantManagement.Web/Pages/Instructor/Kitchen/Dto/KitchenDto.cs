using RestaurantManagement.Web.Pages.Instructor.Kitchen.ViewModel;

namespace RestaurantManagement.Web.Instructor.Pages.Kitchen.Dto
{
    public record KitchenDto(Guid UserId, DateTime Created, List<KitchenItemViewModel> Items);
}
