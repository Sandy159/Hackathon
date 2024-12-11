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
        services.AddControllers();

        services.AddRefitClient<IHRManagerApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://hr_manager:8080"));

        services.AddScoped<EmployeeService>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
