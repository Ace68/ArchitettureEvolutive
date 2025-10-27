namespace BrewUp.Shared.ExternalContracts;

public class SalesOrderRowJson
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    
    public ProductQuantity Quantity { get; set; } = new ();
    public ProductPrice Price { get; set; } = new(); 
}