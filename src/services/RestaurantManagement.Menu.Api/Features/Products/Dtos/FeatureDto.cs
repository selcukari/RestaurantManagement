namespace RestaurantManagement.Menu.Api.Features.Products.Dtos
{
    public class FeatureDto
    {
        public int Calorie { get; set; }
        public float Rating { get; set; }

        public string EducatorFullName { get; set; } = default!;
    }
}
