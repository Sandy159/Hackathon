using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HRManagerApp.Database;
using Microsoft.Extensions.Logging;
using HRManagerApp.Service;

namespace HRManagerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var hrManagerService = services.GetRequiredService<HRManagerService>();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while applying migrations.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        services.AddDbContext<ManagerDbContext>(options =>
                            options.UseNpgsql("Host=hrmanager-db;Database=manager_db;Username=postgres;Password=postgres"));
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
