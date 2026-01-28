using RestaurantManagement.Bus;
using RestaurantManagement.Reporting.Api.Consumers;

namespace RestaurantManagement.Reporting.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services,
       IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;


            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<ReportingCreatedReservationEventConsumer>();

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("reporing-microservice.reporing-reservation-create.queue",
                        e => { e.ConfigureConsumer<ReportingCreatedReservationEventConsumer>(ctx); });


                    // cfg.ConfigureEndpoints(ctx);
                });
            });


            return services;
        }
    }
}
