using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Refit;
using HRManagerApp.Service;
using HRManagerApp.RefitClients;
using HRManagerApp.Database;

namespace HRManagerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiBaseUrl = "http://hrdirector-api:8080";

            // Настройка строки подключения к базе данных с использованием Npgsql
            //services.AddDbContext<ManagerDbContext>(options =>
               // options.UseNpgsql("Host=hrmanager-db;Database=manager_db;Username=postgres;Password=postgres"));
            services.AddScoped<HRManagerService>();

            // Настройка Refit для взаимодействия с HRDirector
            services.AddRefitClient<IHRDirectorApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl));

            // Добавление MVC-контроллеров
            services.AddControllers();

            // Добавление поддержки OpenAPI / Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Страница ошибок для разработки и документация Swagger
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRManager API v1"));
            }

            // Основная конфигурация middleware
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
