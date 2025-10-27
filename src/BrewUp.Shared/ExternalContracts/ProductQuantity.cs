namespace BrewUp.Shared.ExternalContracts;

public class ProductQuantity
{
    public decimal Quantity { get; set; }
    public string UnitOfMeasure { get; set; }  = string.Empty;
}