using Refit;
using RestaurantManagement.Web.Dto;

namespace RestaurantManagement.Web.Services.Refit
{
    public interface IMenuRefitService
    {
        [Get("/api/v1/products")]
        Task<ApiResponse<List<ProductDto>>> GetAllProducts();

        [Get("/api/v1/products/{id}")]
        Task<ApiResponse<ProductDto>> GetProduct(Guid id);


        [Get("/api/v1/categories")]
        Task<ApiResponse<List<MenuDto>>> GetMenuAsync();


        [Get("/api/v1/products/user/{userId}")]
        Task<ApiResponse<List<ProductDto>>> GetProductByUserId(Guid UserId);


        [Multipart]
        [Post("/api/v1/products")]
        Task<ApiResponse<object>> CreateProductAsync(
            [AliasAs("Name")] string Name,
            [AliasAs("Description")] string Description,
            [AliasAs("Price")] decimal Price,
            [AliasAs("Picture")] StreamPart? Picture,
            [AliasAs("MenumId")] string MenumId);


        [Put("/api/v1/products")]
        Task<ApiResponse<object>> UpdaterPoductAsync(UpdateProductRequest request);


        [Delete("/api/v1/products/{id}")]
        Task<ApiResponse<object>> DeleteProductAsync(Guid id);
    }
}
