using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon
{
    public class CompitionWorker : BackgroundService
    {
        private readonly Compition _hackathon;
        private readonly IOptions<ConstantOptions> _constants;
        private readonly IHostApplicationLifetime _host;

        public CompitionWorker(Compition hackathon, IOptions<ConstantOptions> constants, IHostApplicationLifetime host)
        {
            _hackathon = hackathon;
            _constants = constants;
            _host = host;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var constants = _constants.Value;

            var juniors = DataLoader.LoadEmployees(constants.JuniorsFilePath);
            var teamLeads = DataLoader.LoadEmployees(constants.TeamLeadsFilePath);

            _hackathon.SetParticipants(juniors, teamLeads);

            double sum = 0.0;

            for (int i = 0; i < constants.NumberOfHackathons; i++)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                sum += _hackathon.Run();
            }

            double averageHarmonicity = sum / constants.NumberOfHackathons;
            Console.WriteLine($"Средняя гармоничность по {constants.NumberOfHackathons} хакатонам: {averageHarmonicity:F2}");
            _host.StopApplication();
            return Task.CompletedTask;
        }
    }
}
