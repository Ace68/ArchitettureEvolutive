using BrewUp.Sales.Entities.Entities;
using BrewUp.Sales.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.Infrastructure;

public class SalesContext(DbContextOptions<SalesContext> options) : DbContext(options)
{
    public DbSet<SalesOrder> SalesOrder { get; set; }
    public DbSet<SalesOrderRow> SalesOrderRow { get; set; }
    
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

        modelBuilder.ApplyConfiguration(new SalesOrderMapping());
        modelBuilder.ApplyConfiguration(new SalesOrderRowMapping());
        
        modelBuilder.Entity<SalesOrder>()
            .HasMany(s => s.SalesOrderRows)
            .WithOne(r => r.SalesOrder)
            .HasForeignKey(r => r.SalesOrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}