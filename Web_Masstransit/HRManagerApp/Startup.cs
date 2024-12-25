using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using HRManagerApp.Service;
using HRManagerApp.Database;

namespace HRManagerApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("preferences-queue", endpoint =>
                    {
                        endpoint.Consumer<PreferencesConsumer>(ctx);
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddScoped<HRManagerService>();
        }
    }
}
