using BrewUp.Shared.Domain;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Warehouse.SharedKernel.CustomTypes;

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
    
    public void UpdateAvailability(ProductQuantity quantity, WarehouseReference warehouseReference)
    {
        //Availability.UpdateQuantity(quantity);
    }
}