using Microsoft.EntityFrameworkCore;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public class HackathonContext : DbContext
    {
        public HackathonContext(DbContextOptions<HackathonContext> options) : base(options) { }

        public DbSet<CompitionDto> Hackathons { get; set; }
        public DbSet<EmployeeDto> Employees { get; set; }
        public DbSet<WishlistDto> Wishlists { get; set; }
        public DbSet<TeamDto> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDto>()
                .HasKey(e => e.EmployeePk);

            modelBuilder.Entity<TeamDto>()
                .HasKey(t => t.TeamPk);

            modelBuilder.Entity<WishlistDto>()
                .HasKey(w => w.WishlistPk);

            modelBuilder.Entity<CompitionDto>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<WishlistDto>()
                .HasOne(w => w.Employee)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeamDto>()
                .HasOne(t => t.Junior)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeamDto>()
                .HasOne(t => t.TeamLead)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}