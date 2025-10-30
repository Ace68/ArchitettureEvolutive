using BrewUp.Warehouse.Entities.Entities;
using BrewUp.Warehouse.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.Infrastructure;

public class WarehouseContext(DbContextOptions<WarehouseContext> options) : DbContext(options)
{
    public DbSet<Product> Product { get; set; }
    public DbSet<Availability> Availability { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Logging configuration
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter((_, level) => level == LogLevel.Information)
                .AddConsole();
        });

        optionsBuilder.UseLoggerFactory(loggerFactory);
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductMapping());
        modelBuilder.ApplyConfiguration(new AvailabilityMapping());
        
        modelBuilder.Entity<Product>()
            .HasMany(s => s.Availabilities)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}