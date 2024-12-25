using CommonLibrary.Contracts;
using EmployeeApp.RefitClients;
using EmployeeApp.Utils;

namespace EmployeeApp.Service;

public class EmployeeService
{
    private readonly IHRManagerApi _hrManagerApi;

    public EmployeeService(IHRManagerApi hrManagerApi)
    {
        _hrManagerApi = hrManagerApi;
    }

    public async Task ProcessWishlistsAsync()
    {
        var employeeIdString = Environment.GetEnvironmentVariable("EMPLOYEE_ID");
        int.TryParse(employeeIdString, out int employeeId);
        Console.WriteLine($"Employee ID: {employeeId}");

        var role = Environment.GetEnvironmentVariable("ROLE");
        Console.WriteLine($"Role: {role}");

        var juniors = DataLoader.LoadEmployees("data/juniors.csv");
        Console.WriteLine($"Loaded {juniors.Count} juniors.");

        var teamLeads = DataLoader.LoadEmployees("data/teamleads.csv");
        Console.WriteLine($"Loaded {teamLeads.Count} team leads.");

        var preferenceMessage = WishlistGenerator.GenerateWishlist(juniors, teamLeads, employeeId, role);
        Console.WriteLine($"Generated preference message for EmployeeId: {employeeId}");

        await _hrManagerApi.SubmitPreferencesAsync(preferenceMessage);
        Console.WriteLine("Preferences submitted to HR Manager API.");
    }
}
