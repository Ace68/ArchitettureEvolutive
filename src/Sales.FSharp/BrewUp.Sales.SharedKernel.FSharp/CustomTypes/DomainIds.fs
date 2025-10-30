namespace BrewUp.Sales.SharedKernel.FSharp.CustomTypes

open System
open Muflone.Core

// Domain IDs inheriting from Muflone's DomainId
type SalesOrderId private (value: string) =
    inherit DomainId(value)
    
    static member New() = SalesOrderId(Guid.NewGuid().ToString())
    static member From(value: string) = 
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "SalesOrderId cannot be null or whitespace"
        SalesOrderId(value)

type ProductId private (value: string) =
    inherit DomainId(value)
    
    static member New() = ProductId(Guid.NewGuid().ToString())
    static member From(value: string) = 
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "ProductId cannot be null or whitespace"
        ProductId(value)