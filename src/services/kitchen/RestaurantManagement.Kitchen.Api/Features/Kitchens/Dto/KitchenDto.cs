namespace RestaurantManagement.Kitchen.Api.Features.Kitchens.Dto
{
    public record KitchenDto(string UserFullName, DateTime Created, List<KitchenItem> Items);
}
