using EmployeeApp.RefitClients;
using EmployeeApp.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace EmployeeApp;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var apiBaseUrl = "http://hrmanager-api:8080";

        services.AddControllers();

        services.AddRefitClient<IHRManagerApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl));

        services.AddScoped<EmployeeService>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
