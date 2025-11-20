
namespace RestaurantManagement.Menu.Api.Features.Menus
{
    public class MenuMapping: Profile
    {
        public MenuMapping()
        {
            CreateMap<Menum, MenuDto>().ReverseMap();
        }
    }
}
