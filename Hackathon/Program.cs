using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hackathon;

class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var dataLoader = new DataLoader();
                var constants = new Constants();

                services.AddSingleton<DataLoader>(); 
                services.AddSingleton<Constants>(); 

                services.AddTransient<HRManager>();
                services.AddTransient<HRDirector>();
                services.AddTransient<ITeamBuildingStrategy, SimpleTeamBuildingStrategy>(); 

                services.AddSingleton<List<Junior>>(provider =>
                    dataLoader.LoadEmployees(constants.JuniorsFilePath, (id, name) => new Junior(id, name)));

                services.AddSingleton<List<TeamLead>>(provider =>
                    dataLoader.LoadEmployees(constants.TeamLeadsFilePath, (id, name) => new TeamLead(id, name)));

                services.AddSingleton<Compition>();
                services.AddSingleton<CompitionWorker>(); 

                services.AddHostedService<CompitionWorker>();
            }).Build();
            host.Run();
    }
}
