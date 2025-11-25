using Asp.Versioning.Builder;
using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RestaurantManagement.Shared.Extensions
{
    public static class VersioningExt
    {
        public static IServiceCollection AddVersioningExt(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                //options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader(),
                //    new QueryStringApiVersionReader(), new UrlSegmentApiVersionReader());
            }).AddApiExplorer(options => // swagger ın v1,v2 gibi sıralama yapması icin
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });


            return services;
        }


        public static ApiVersionSet AddVersionSetExt(this WebApplication app)
        {
            var apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0))
                .HasApiVersion(new ApiVersion(1, 2))
                .HasApiVersion(new ApiVersion(1, 0))
                .ReportApiVersions()
                .Build();
            return apiVersionSet;
        }
    }
}
