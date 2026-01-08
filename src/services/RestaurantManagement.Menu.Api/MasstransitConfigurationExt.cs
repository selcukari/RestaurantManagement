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
                configure.AddConsumer<OrderCreatedItemsEventConsumer>();

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("menu-microservice.product-picture-uploaded.queue",
                        e => { e.ConfigureConsumer<ProductPictureUploadedEventConsumer>(ctx); });

                    cfg.ReceiveEndpoint("menu-microservice.product-quantity-updated.queue",
                        e => { e.ConfigureConsumer<OrderCreatedItemsEventConsumer>(ctx); });


                    // cfg.ConfigureEndpoints(ctx);
                });
            });


            return services;
        }
    }
}
