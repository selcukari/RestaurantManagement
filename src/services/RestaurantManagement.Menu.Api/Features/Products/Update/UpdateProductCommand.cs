namespace RestaurantManagement.Menu.Api.Features.Products.Update;

    public record UpdateProductCommand(Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    int Calorie,
    string? ImageUrl,
    Guid MenumId) : IRequestByServiceResult;
