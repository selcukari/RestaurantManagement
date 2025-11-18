namespace RestaurantManagement.Menu.Api.Features.Menus.Create
{
    public record CreateMenuCommand(string Name) : IRequestByServiceResult<CreateMenuResponse>;
}
