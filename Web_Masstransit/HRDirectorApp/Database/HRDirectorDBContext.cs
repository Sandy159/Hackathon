using Microsoft.EntityFrameworkCore;
using CommonLibrary.Contracts;

namespace HRDirectorApp.Database;

public class HRDirectorDbContext : DbContext
{
    public HRDirectorDbContext(DbContextOptions<HRDirectorDbContext> options) : base(options) { }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=hrdirector-db;Database=director_db;Username=postgres;Password=postgres");
        }
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HarmonyRecord>().HasKey(h => h.Id); // Установка первичного ключа
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<HarmonyRecord> HarmonyRecords { get; set; } // Таблица для записи хакатонов
}
