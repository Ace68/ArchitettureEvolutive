using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.Entities.Entities;

public class SalesOrderRow : DtoBase
{
    public string SalesOrderId { get; private set; } = string.Empty;
    public string ProductId { get; private set; } = string.Empty;
    public string ProductName { get; private set; } = string.Empty;
    
    public double Quantity { get; private set; }
    public string UnitOfMeasure { get; private set; } = string.Empty;
    
    public double Price { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    
    public virtual SalesOrder SalesOrder { get; set; } 
    
    protected SalesOrderRow() 
    { }

    internal static SalesOrderRow Create(SalesOrderId salesOrderId, ProductId productId, ProductName productName,
        ProductQuantity quantity, ProductPrice price)
    {
        return new SalesOrderRow(salesOrderId, productId, productName, quantity, price);
    }

    private SalesOrderRow(SalesOrderId salesOrderId, ProductId productId, ProductName productName,
        ProductQuantity quantity, ProductPrice price)
    {
        Id = Guid.NewGuid().ToString();
        SalesOrderId = salesOrderId.Value;
        
        ProductId = productId.Value;
        ProductName = productName.Value;
        
        Quantity = (double) quantity.Quantity;
        UnitOfMeasure = quantity.UnitOfMeasure;
        
        Price = (double) price.Price;
        Currency = price.Currency;
    }
}