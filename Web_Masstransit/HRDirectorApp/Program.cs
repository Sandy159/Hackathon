using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using HRDirectorApp.Database;
using HRDirectorApp.Service;
using Microsoft.Extensions.Logging;

namespace HRdirectorApp
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
                    var hrDirectorService = services.GetRequiredService<HRDirectorService>();
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
                        services.AddDbContext<HRDirectorDbContext>(options =>
                            options.UseNpgsql("Host=hrdirector-db;Database=director_db;Username=postgres;Password=postgres"));
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
