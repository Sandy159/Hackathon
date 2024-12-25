using CommonLibrary.Contracts;

namespace HRManagerApp.Database;

using Microsoft.EntityFrameworkCore;

public class ManagerDbContext : DbContext
{
    public ManagerDbContext(DbContextOptions<ManagerDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=hrmanager-db;Database=manager_db;Username=postgres;Password=postgres");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeePreference>(entity =>
        {
            entity.HasKey(e => e.HackathonId);
            entity.Property(e => e.EmployeeId).IsRequired();
            entity.Property(e => e.Role).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Preferences).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<EmployeePreference> EmployeePreferences { get; set; }
}
