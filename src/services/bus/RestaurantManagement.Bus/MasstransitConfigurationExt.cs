using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace RestaurantManagement.Bus
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddCommonMasstransitExt(this IServiceCollection services,
        IConfiguration configuration)
        {
            // Fix: Use Get<T> extension method from Microsoft.Extensions.Configuration.Binder
            // Add the required using directive: using Microsoft.Extensions.Configuration;
            var busOptions = configuration.GetSection(nameof(BusOption));

            var Address = busOptions.GetSection("Address").Value;
            var Port = busOptions.GetSection("Port").Value;
            var UserName = busOptions.GetSection("UserName").Value;
            var Password = busOptions.GetSection("Password").Value;

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{Address}:{Port}"), host =>
                    {
                        host.Username(UserName!);
                        host.Password(Password!);
                    });

                    cfg.ConfigureEndpoints(ctx);

                    //cfg.ReceiveEndpoint("basket-microservice.create-order-event.queue",
                    //    e => { e.ConfigureConsumer<CreateOrderEventConsumer>(context); });
                });
            });

            return services;
        }
    }
}
