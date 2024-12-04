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
        private readonly CompitionMenu _menu;
        private readonly IHostApplicationLifetime _host;

        public CompitionWorker(CompitionMenu menu, IHostApplicationLifetime host)
        {
            _menu = menu;
            _host = host;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _menu.Run();
            _host.StopApplication();
        }
    }
}
