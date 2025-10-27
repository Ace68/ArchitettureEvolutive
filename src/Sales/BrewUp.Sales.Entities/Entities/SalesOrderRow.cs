using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.Entities.Entities;

public class SalesOrderRow : DtoBase
{
    public string ProductId { get; private set; } = string.Empty;
    public string ProductName { get; private set; } = string.Empty;
    
    public double Quantity { get; private set; }
    public string UnitOfMeasure { get; private set; } = string.Empty;
    
    public double Price { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    
    protected SalesOrderRow() 
    { }
    
    internal static SalesOrderRow Create(ProductId productId, ProductName productName, Quantity quantity, Price price)
    {
        return new SalesOrderRow(productId, productName, quantity, price);
    }

    private SalesOrderRow(ProductId productId, ProductName productName, Quantity quantity, Price price)
    {
        // _productId = productId;
        // _productName = productName;
        //
        // _quantity = quantity;
        // _price = price;
    }
}