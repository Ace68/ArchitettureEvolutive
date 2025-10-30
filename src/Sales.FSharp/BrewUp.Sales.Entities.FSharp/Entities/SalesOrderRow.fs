namespace BrewUp.Sales.Entities.FSharp.Entities

open System
open BrewUp.Shared.FSharp.Domain
open BrewUp.Shared.FSharp.ExternalContracts
open BrewUp.Sales.SharedKernel.FSharp.CustomTypes

type SalesOrderRow = {
    Id: string
    SalesOrderId: string
    ProductId: string
    ProductName: string
    Quantity: float
    UnitOfMeasure: string
    Price: decimal
    Currency: string
}

module SalesOrderRow =
    let create (salesOrderId: SalesOrderId) (productId: ProductId) (productName: ProductName) 
               (quantity: ProductQuantity) (price: ProductPrice) =
        {
            Id = Guid.NewGuid().ToString()
            SalesOrderId = salesOrderId.Value
            ProductId = productId.Value
            ProductName = productName.Value
            Quantity = quantity.Quantity
            UnitOfMeasure = quantity.UnitOfMeasure
            Price = price.Price
            Currency = price.Currency
        }