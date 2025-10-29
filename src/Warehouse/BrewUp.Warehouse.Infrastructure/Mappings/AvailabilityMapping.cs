using BrewUp.Warehouse.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrewUp.Warehouse.Infrastructure.Mappings;

public class AvailabilityMapping : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder.ToTable("Availability");
        builder.HasKey(x => new { x.ProductId, x.WarehouseReference });
        
        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasMaxLength(36);
        builder.Property(x => x.WarehouseReference)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Quantity)
            .IsRequired();
    }
}