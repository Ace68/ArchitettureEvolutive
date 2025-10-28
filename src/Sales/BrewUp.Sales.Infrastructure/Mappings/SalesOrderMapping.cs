using BrewUp.Sales.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrewUp.Sales.Infrastructure.Mappings;

public class SalesOrderMapping : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.ToTable("SalesOrder", "dbo");
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .IsRequired()
            .HasMaxLength(36);
        builder.Property(t => t.SalesOrderNumber)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(t => t.CustomerId)
            .IsRequired()
            .HasMaxLength(36);
        builder.Property(t => t.CustomerName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(t => t.Status)
            .IsRequired()
            .HasMaxLength(20);
    }
}