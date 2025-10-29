using BrewUp.Warehouse.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrewUp.Warehouse.Infrastructure.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(36);
        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.ProductDescription)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(x => x.ProductType)
            .IsRequired()
            .HasMaxLength(100);
    }
}