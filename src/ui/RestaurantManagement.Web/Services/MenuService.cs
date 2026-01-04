using Refit;
using RestaurantManagement.Web.Dto;
using RestaurantManagement.Web.Services.Refit;
using RestaurantManagement.Web.ViewModel;
using System.Text.Json;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace RestaurantManagement.Web.Services
{
    public class MenuService(
    IMenuRefitService menuRefitService,
    UserService userService,
    ILogger<MenuService> logger)
    {
        public async Task<ServiceResult<List<ProductViewModel>>> GetAllProductsAsync()
        {
            var productAsResult = await menuRefitService.GetAllProducts();

            if (!productAsResult.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(productAsResult.Error.Content!);
                logger.LogError("Error occurred while fetching products");
                //logger.LogProblemDetails(productAsResult.Error);

                return ServiceResult<List<ProductViewModel>>.Error(
                    "Failed to retrieve product data. Please try again later.");
            }


            var products = productAsResult.Content!;

            var menusViewModel = products.Select(c =>
                new ProductViewModel(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.Price,
                    c.ImageUrl,
                    c.Created.ToLongDateString(),
                    c.Feature.EducatorFullName,
                    c.Menum.Name,
                    c.Menum.Id,
                    c.Feature.Duration,
                    c.Feature.Rating)).ToList();

            return ServiceResult<List<ProductViewModel>>.Success(menusViewModel);
        }


        public async Task<ServiceResult<ProductViewModel>> GetProduct(Guid productId)
        {
            var response = await menuRefitService.GetProduct(productId);

            if (!response.IsSuccessStatusCode)
                return ServiceResult<ProductViewModel>.FailFromProblemDetails(response.Error);


            var product = response.Content!;
            var courseViewModel = new ProductViewModel(product.Id, product.Name, product.Description, product.Price,
                product.ImageUrl, product.Created.ToLongDateString(), product.Feature.EducatorFullName, product.Menum.Name,
                product.Menum.Id, product.Feature.Duration, product.Feature.Rating);

            return ServiceResult<ProductViewModel>.Success(courseViewModel);
        }


        public async Task<ServiceResult<List<MenuViewModel>>> GetMenusAsync()
        {
            var response = await menuRefitService.GetMenusAsync();
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while fetching categories");
                return ServiceResult<List<MenuViewModel>>.Error("Fail to retrieve menu. Please try again later");
            }

            var menus = response!.Content!
                .Select(c => new MenuViewModel(c.Id, c.Name))
                .ToList();
            return ServiceResult<List<MenuViewModel>>.Success(menus);
        }

        public async Task<ServiceResult> CreateProductAsync(CreateProductViewModel model)
        {
            StreamPart? pictureStreamPart = null;
            await using var stream = model.PictureFormFile?.OpenReadStream();

            if (model.PictureFormFile is not null && model.PictureFormFile.Length > 0)
                pictureStreamPart =
                    new StreamPart(stream!, model.PictureFormFile.FileName, model.PictureFormFile.ContentType);


            var response = await menuRefitService.CreateProductAsync(
                model.Name,
                model.Description,
                model.Price,
                pictureStreamPart,
                model.MenuId.ToString()!
            );

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while creating course");
                return ServiceResult.Error("Fail to create course. Please try again later");
            }


            return ServiceResult.Success();
        }

        public async Task<ServiceResult> UpdateProductAsync(UpdateProductViewModel model)
        {
            StreamPart? pictureStreamPart = null;
            await using var stream = model.PictureFormFile?.OpenReadStream();

            if (model.PictureFormFile is not null && model.PictureFormFile.Length > 0)
                pictureStreamPart =
                    new StreamPart(stream!, model.PictureFormFile.FileName, model.PictureFormFile.ContentType);


            var response = await menuRefitService.UpdaterPoductAsync(
                new UpdateProductRequest(
                    model.Id,
                    model.Name,
                    model.Description,
                    model.Price,
                    model.ExistingPictureUrl,
                    model.MenuId
                )
            );

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while creating course");
                return ServiceResult.Error("Fail to create course. Please try again later");
            }


            return ServiceResult.Success();
        }


        public async Task<ServiceResult<List<ProductViewModel>>> GetProductByUserId()
        {
            var course = await menuRefitService.GetProductByUserId(userService.UserId);

            if (!course.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(course.Error.Content!);
                logger.LogError("Error occurred while fetching courses by user id");
                return ServiceResult<List<ProductViewModel>>.Error("Fail to retrieve courses. Please try again later");
            }

            var products = course!.Content!
                .Select(c => new ProductViewModel(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.Price,
                    c.ImageUrl,
                    c.Created.ToLongDateString(),
                    c.Feature.EducatorFullName,
                    c.Menum.Name,
                    c.Menum.Id,
                    c.Feature.Duration,
                    c.Feature.Rating
                ))
                .ToList();

            return ServiceResult<List<ProductViewModel>>.Success(products);
        }

        public async Task<ServiceResult> DeleteAsync(Guid ProductId)
        {
            var response = await menuRefitService.DeleteProductAsync(ProductId);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while deleting course");
                return ServiceResult.Error("Fail to delete Product. Please try again later");
            }

            return ServiceResult.Success();
        }
    }
}
