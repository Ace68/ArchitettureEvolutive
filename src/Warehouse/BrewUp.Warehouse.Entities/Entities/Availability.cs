using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using BrewUp.Warehouse.SharedKernel.CustomTypes;

namespace BrewUp.Warehouse.Entities.Entities;

public class Availability : DtoBase
{
    public string ProductId { get; private set; } = string.Empty;
    public string WarehouseReference { get; private set; } = string.Empty;
    
    public virtual Product Product { get; private set; } = null!;
    
    public double Quantity { get; private set; }
    
    protected Availability()
    {}

    public static Availability Create(ProductId productId, WarehouseReference warehouseReference, ProductQuantity quantity)
    {
        return new Availability(productId, warehouseReference, quantity);
    }
    
    private Availability(ProductId productId, WarehouseReference warehouseReference, ProductQuantity quantity)
    {
        ProductId = productId.Value;
        WarehouseReference = warehouseReference.Value;
        Quantity = (double) quantity.Quantity;
    }
    
    internal void UpdateAvailability(ProductQuantity quantity)
    {
        Quantity = (double) quantity.Quantity;
    }
}