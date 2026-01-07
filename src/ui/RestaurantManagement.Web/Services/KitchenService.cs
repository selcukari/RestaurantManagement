using RestaurantManagement.Web.Pages.Instructor.Kitchen.ViewModel;
using RestaurantManagement.Web.Services.Refit;

namespace RestaurantManagement.Web.Services
{
    public class KitchenService(
    IKitchenRefitService kitchenRefitService,
    UserService userService,
    ILogger<MenuService> logger)
    {
        public async Task<ServiceResult<List<KitchenViewModel>>> GetAllProductsAsync()
        {
            var response = await kitchenRefitService.GetAllKitchens();

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(new EventId(), null, response.Error);
                return ServiceResult<List<KitchenViewModel>>.Error(
                    "An error occurred while getting the Kitchen");
            }

            var kitchenList = new List<KitchenViewModel>();


            foreach (var kitchenResponse in response.Content)
            {
                var newKitchen =
                    new KitchenViewModel(kitchenResponse.UserId, kitchenResponse.Created.ToLongDateString());

                foreach (var kitchenItem in kitchenResponse.Items)
                    newKitchen.AddItem(kitchenItem.Id, kitchenItem.Name, kitchenItem.Quantity);

                kitchenList.Add(newKitchen);
            }

            return ServiceResult<List<KitchenViewModel>>.Success(kitchenList);
        }
    }
}
