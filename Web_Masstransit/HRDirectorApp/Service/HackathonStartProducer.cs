using CommonLibrary.Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HRDirectorApp.Service;

public class HackathonStartProducer(IBus _bus) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 10; i++)
        {
            await _bus.Publish(new HackathonAnnouncementMessage{HackathonId = i});
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}