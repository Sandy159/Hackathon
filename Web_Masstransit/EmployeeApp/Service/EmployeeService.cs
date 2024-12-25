using CommonLibrary.Contracts;
using EmployeeApp.Utils;
using MassTransit;

namespace EmployeeApp.Service;

public class EmployeeService
{
    private readonly IBusControl _busControl;
    private int _currentHackathonId; 

    public EmployeeService(IBusControl busControl)
    {
        _busControl = busControl;
    }

    // Обработка сообщения о начале хакатона
    public Task OnHackathonStartedAsync(int hackathonId)
    {
        _currentHackathonId = hackathonId;
        Console.WriteLine($"Hackathon started with ID: {_currentHackathonId}");
        return Task.CompletedTask;
    }

    public async Task ProcessWishlistsAsync()
    {
        if (_currentHackathonId == null)
        {
            Console.WriteLine("Hackathon ID is not set. Cannot process wishlists.");
            return;
        }

        var employeeIdString = Environment.GetEnvironmentVariable("EMPLOYEE_ID");
        int.TryParse(employeeIdString, out int employeeId);
        Console.WriteLine($"Employee ID: {employeeId}");

        var role = Environment.GetEnvironmentVariable("ROLE");
        Console.WriteLine($"Role: {role}");

        var juniors = DataLoader.LoadEmployees("data/juniors.csv");
        Console.WriteLine($"Loaded {juniors.Count} juniors.");

        var teamLeads = DataLoader.LoadEmployees("data/teamleads.csv");
        Console.WriteLine($"Loaded {teamLeads.Count} team leads.");

        var preferenceMessage = WishlistGenerator.GenerateWishlist(juniors, teamLeads, _currentHackathonId, employeeId, role);
        preferenceMessage.HackathonId = _currentHackathonId;
        Console.WriteLine($"Generated preference message for EmployeeId: {employeeId}");

        await _busControl.Publish(preferenceMessage);
        Console.WriteLine("Preferences submitted to RabbitMQ.");
    }
}
