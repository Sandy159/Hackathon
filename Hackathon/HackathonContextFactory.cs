using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Hackathon
{
    public class HackathonContextFactory : IDesignTimeDbContextFactory<HackathonContext>
    {
        public HackathonContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HackathonContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hackathon;Username=postgres;Password=postgres");

            return new HackathonContext(optionsBuilder.Options);
        }
    }
}
