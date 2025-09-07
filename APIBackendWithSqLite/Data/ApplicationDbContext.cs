using APIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerIdCode);

        modelBuilder.Entity<Unit>()
            .HasKey(c => c.UnitIdCode);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Locations)
            .WithOne(l => l.Customer)
            .HasForeignKey(l => l.CustomerIdCode);

        modelBuilder.Entity<Location>()
            .HasMany(l => l.Units)
            .WithOne(u => u.Location)
            .HasForeignKey(u => u.LocationId);

        modelBuilder.Entity<Unit>()
            .HasMany(u => u.UnitHistories)
            .WithOne(h => h.Unit)
            .HasForeignKey(h => h.UnitIdCode);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Logs> Logs { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<UnitHistory> UnitHistories { get; set; }
}
