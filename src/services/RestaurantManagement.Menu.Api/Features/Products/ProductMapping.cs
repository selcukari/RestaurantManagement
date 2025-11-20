
using RestaurantManagement.Menu.Api.Features.Products.Create;
using RestaurantManagement.Menu.Api.Features.Products.Dtos;

namespace RestaurantManagement.Menu.Api.Features.Products
{
    public class ProductMapping: Profile
    {
        public ProductMapping()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
