namespace BrewUp.Shared.FSharp.ExternalContracts

open System

type SalesOrderJson = {
    Id: string
    OrderNumber: string
    OrderDate: DateTime
    CustomerId: string
    CustomerName: string
    DeliveryDate: DateTime
    Rows: SalesOrderRowJson array
    Status: string
}