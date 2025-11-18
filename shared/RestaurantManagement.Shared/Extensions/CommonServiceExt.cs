using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Shared.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using RestaurantManagement.Shared.ExceptionHandlers;

namespace RestaurantManagement.Shared.Extensions
{
    public static class CommonServiceExt
    {
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
        {
            services.AddHttpContextAccessor();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddAutoMapper(assembly);
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}
