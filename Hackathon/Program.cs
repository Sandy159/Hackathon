using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Hackathon;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using Nsu.HackathonProblem.Contracts;

class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<ConstantOptions>(hostContext.Configuration.GetSection("Constants"));

                services.AddTransient<HRManager>();
                services.AddTransient<HRDirector>();
                services.AddTransient<ITeamBuildingStrategy, SimpleTeamBuildingStrategy>(); 
                services.AddTransient<Compition>();

                //services.AddSingleton<CompitionMenu>();

                //services.AddDbContext<HackathonContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=hackathon;Username=postgres;Password=postgres"));

                /*services.AddMediatR(cfg => 
                            {
                                cfg.RegisterServicesFromAssemblyContaining<Program>();
                            }).BuildServiceProvider();*/

                services.AddHostedService<CompitionWorker>();
            }).Build();
        host.Run();
    }
}
