namespace BrewUp.Shared.FSharp.ExternalContracts

type SalesOrderRowJson = {
    ProductId: string
    ProductName: string
    Quantity: ProductQuantity
    Price: ProductPrice
}