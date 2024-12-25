using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HRDirectorApp.Database
{
    public class HRDirectorDbContextFactory : IDesignTimeDbContextFactory<HRDirectorDbContext>
    {
        public HRDirectorDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HRDirectorDbContext>();
            //optionsBuilder.UseNpgsql("Host=hrdirector-db;Database=director_db;Username=postgres;Password=postgres");

            return new HRDirectorDbContext(optionsBuilder.Options);
        }
    }
}
