using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Hackathon;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


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

                services.AddSingleton<Compition>();
                services.AddHostedService<CompitionWorker>();
            }).Build();
        host.Run();
    }
}
