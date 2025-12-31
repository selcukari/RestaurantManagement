using Microsoft.AspNetCore.Razor.TagHelpers;
using RestaurantManagement.Web.Options;

namespace RestaurantManagement.Web.TagHelpers
{
    public class ProductThumbnailPictureTagHelper(MicroserviceOption microserviceOption) : TagHelper
    {
        public string? Src { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";

            var blankCourseThumbnailImagePath = "/images/img-thumbnail.jpg";

            if (string.IsNullOrEmpty(Src))
            {
                output.Attributes.SetAttribute("src", blankCourseThumbnailImagePath);
            }
            else
            {
                var courseThumbnailImagePath = $"{microserviceOption.File.BaseAddress}/{Src}";


                output.Attributes.SetAttribute("src", courseThumbnailImagePath);
            }


            return base.ProcessAsync(context, output);
        }
    }
}
