using RestaurantManagement.Kitchen.Api.Features.Kitchens.Dto;

namespace RestaurantManagement.Kitchen.Api.Features.Kitchens
{
    public class KitchenMapping: Profile
    {
        public KitchenMapping()
        {
            CreateMap<Kitchen, KitchenDto>().ReverseMap();
        }
    }
}
