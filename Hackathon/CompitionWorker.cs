using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon
{
    public class CompitionWorker : BackgroundService
    {
        private readonly Compition _hackathon;
        private readonly Constants _constants;
        private readonly IHostApplicationLifetime _host;

        public CompitionWorker(Compition hackathon, Constants constants, IHostApplicationLifetime host)
        {
            _hackathon = hackathon;
            _constants = constants;
            _host = host;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            double averageHarmonicity = 0.0;
            double sum = 0.0;

            for (int i = 0; i < _constants.NumberOfHackathons; i++)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                sum += _hackathon.Run();
                await Task.Delay(1, stoppingToken);
            }

            averageHarmonicity = sum / _constants.NumberOfHackathons;
            Console.WriteLine($"Средняя гармоничность по {_constants.NumberOfHackathons} хакатонам: {averageHarmonicity:F2}");
            _host.StopApplication();
        }
    }
}
