namespace BrewUp.Shared.FSharp.ExternalContracts

open System

type CreateSalesOrderJson = {
    OrderNumber: string
    OrderDate: DateTime
    CustomerId: string
    CustomerName: string
    DeliveryDate: DateTime
    Rows: SalesOrderRowJson array
}