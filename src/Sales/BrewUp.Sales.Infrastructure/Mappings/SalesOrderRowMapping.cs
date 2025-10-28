using BrewUp.Sales.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrewUp.Sales.Infrastructure.Mappings;

public class SalesOrderRowMapping : IEntityTypeConfiguration<SalesOrderRow>
{
    public void Configure(EntityTypeBuilder<SalesOrderRow> builder)
    {
        builder.ToTable("SalesOrderRow", "dbo");
        builder.HasKey(t => t.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(36);
        builder.Property(x => x.SalesOrderId)
            .IsRequired()
            .HasMaxLength(36);
        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasMaxLength(36);
        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Quantity)
            .IsRequired();
        builder.Property(x => x.UnitOfMeasure)
            .IsRequired()
            .HasMaxLength(10);
        builder.Property(x => x.Price)
            .IsRequired();
        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(3);
    }
}