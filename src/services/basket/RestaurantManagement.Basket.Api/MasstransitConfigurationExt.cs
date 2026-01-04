
using MassTransit;
using RestaurantManagement.Basket.Api.Consumers;
using RestaurantManagement.Bus;

namespace RestaurantManagement.Basket.Api;

    public static class MasstransitConfigurationExt
    {
    public static IServiceCollection AddMasstransitExt(this IServiceCollection services,
    IConfiguration configuration)
    {
        var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;


        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<OrderCreatedEventConsumer>();
            configure.AddConsumer<ProductNameChangedEventConsumer>();

            configure.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                {
                    host.Username(busOptions.UserName);
                    host.Password(busOptions.Password);
                });

                cfg.ReceiveEndpoint("basket-microservice.order-created.queue",
                    e => { e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx); });

                cfg.ReceiveEndpoint("basket-microservice.product-name-changed.queue", e =>
                {
                    e.ConfigureConsumer<ProductNameChangedEventConsumer>(ctx);
                });


                // cfg.ConfigureEndpoints(ctx);
            });
        });


        return services;
    }
}
