using RestaurantManagement.Bus;
using RestaurantManagement.Kitchen.Api.Consumers;

namespace RestaurantManagement.Kitchen.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services,
    IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;


            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<OrderCreatedForKitchenEventConsumer> ();

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("kitchen-microservice.kitchen-created.queue",
                        e => { e.ConfigureConsumer<OrderCreatedForKitchenEventConsumer>(ctx); });

                    // cfg.ConfigureEndpoints(ctx);
                });
            });


            return services;
        }
    }
}
