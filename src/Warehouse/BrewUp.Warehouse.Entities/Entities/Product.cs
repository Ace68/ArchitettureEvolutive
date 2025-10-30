using BrewUp.Shared.Domain;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Warehouse.SharedKernel.CustomTypes;
using BrewUp.Warehouse.SharedKernel.Messages.Events;

namespace BrewUp.Warehouse.Entities.Entities;

public class Product : BrewUpAggregateRoot
{
    public string ProductName { get; private set; } = string.Empty;
    public string ProductDescription { get; private set; } = string.Empty;
    public string ProductType { get; private set; } = string.Empty;

    public virtual ICollection<Availability> Availabilities { get; private set; } = [];
    
    protected Product()
    {}

    public static Product Create(ProductId productId, ProductName productName, ProductDescription productDescription,
        ProductType productType)
    {
        return new Product(productId, productName, productDescription, productType);
    }

    private Product(ProductId productId, ProductName productName, ProductDescription productDescription,
        ProductType productType)
    {
        Id = productId.Value;
        ProductName = productName.Value;
        ProductDescription = productDescription.Value;
        ProductType = productType.Value;
    }

    public void PrepareSalesOrder(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber,
        IEnumerable<SalesOrderRowJson> rows, Guid correlationId)
    {
        IEnumerable<SalesOrderRowJson> productAvailable = [];
        foreach (var row in rows)
        {
            Availability? availability = Availabilities.FirstOrDefault(a => a.ProductId == row.ProductId);
            if (availability == null) continue;
            
            productAvailable = productAvailable.Concat(new List<SalesOrderRowJson>
            {
                row
            });
                
            availability.UpdateAvailability(row.Quantity with {Quantity = availability.Quantity - row.Quantity.Quantity});
        }

        RaiseEvent(new AvailabilityUpdated(salesOrderId, salesOrderNumber, productAvailable, correlationId));
    }
}