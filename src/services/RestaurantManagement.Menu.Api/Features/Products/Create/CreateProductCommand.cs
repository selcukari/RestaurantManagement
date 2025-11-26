namespace RestaurantManagement.Menu.Api.Features.Products.Create
{
    public record CreateProductCommand: IRequestByServiceResult<Guid>
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public decimal Price { get; init; }
        public Guid MenumId { get; init; }

        public IFormFile? Picture { get; set; }
    }
}
