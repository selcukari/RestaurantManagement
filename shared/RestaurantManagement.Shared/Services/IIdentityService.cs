namespace RestaurantManagement.Shared.Services
{
    public class IIdentityService
    {
        Guid UserId { get; }
        string UserName { get; }

        List<string> Roles { get; }
    }
}
