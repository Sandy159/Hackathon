using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HRManagerApp.Database
{
    public class HRManagerDbContextFactory : IDesignTimeDbContextFactory<ManagerDbContext>
    {
        public ManagerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ManagerDbContext>();
            //optionsBuilder.UseNpgsql("Host=hrmanager-db;Database=manager_db;Username=postgres;Password=postgres");

            return new ManagerDbContext(optionsBuilder.Options);
        }
    }
}
