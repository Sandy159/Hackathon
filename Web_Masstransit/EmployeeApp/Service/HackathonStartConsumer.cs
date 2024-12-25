using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CommonLibrary.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeApp.Service;

public class HackathonStartConsumer : BackgroundService
{
    private readonly ILogger<HackathonStartConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public HackathonStartConsumer(ILogger<HackathonStartConsumer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting HackathonStartConsumer...");

        var busControl = _serviceProvider.GetRequiredService<IBusControl>();

        busControl.ConnectReceiveEndpoint("hackathon-start", endpoint =>
        {
            endpoint.Handler<HackathonAnnouncementMessage>(async context =>
            {
                var hackathonEvent = context.Message;
                _logger.LogInformation($"Hackathon started: ID = {hackathonEvent.HackathonId}");

                using var scope = _serviceProvider.CreateScope();
                var employeeService = scope.ServiceProvider.GetRequiredService<EmployeeService>();

                await employeeService.OnHackathonStartedAsync(hackathonEvent.HackathonId);
            });
        });

        await busControl.StartAsync(stoppingToken);
        _logger.LogInformation("HackathonStartConsumer started.");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        var busControl = _serviceProvider.GetRequiredService<IBusControl>();
        await busControl.StopAsync(cancellationToken);
        _logger.LogInformation("HackathonStartConsumer stopped.");
    }
}
