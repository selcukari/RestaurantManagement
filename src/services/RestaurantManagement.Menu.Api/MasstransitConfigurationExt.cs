using RestaurantManagement.Bus;
using RestaurantManagement.Menu.Api.Consumers;

namespace RestaurantManagement.Menu.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services,
       IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;


            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<ProductPictureUploadedEventConsumer>();


                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("menu-microservice.course-picture-uploaded.queue",
                        e => { e.ConfigureConsumer<ProductPictureUploadedEventConsumer>(ctx); });


                    // cfg.ConfigureEndpoints(ctx);
                });
            });


            return services;
        }
    }
}
