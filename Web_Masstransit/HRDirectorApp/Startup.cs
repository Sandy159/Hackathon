using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.Hosting;

using HRDirectorApp.Service;
using HRDirectorApp.Database;
using CommonLibrary.Contracts;

namespace HRdirectorApp
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

                    cfg.ReceiveEndpoint("team-queue", endpoint =>
                    {
                        endpoint.Consumer<TeamConsumer>(ctx);
                    });
                });
            });

            services.AddMassTransitHostedService();
            services.AddHostedService<HackathonStartProducer>();

            services.AddScoped<HRDirectorService>();
        }
    }
}
