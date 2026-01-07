namespace RestaurantManagement.Kitchen.Api.Features.Kitchens.Dto
{
    public record KitchenDto(Guid UserId, DateTime Created, List<KitchenItem> Items);
}
