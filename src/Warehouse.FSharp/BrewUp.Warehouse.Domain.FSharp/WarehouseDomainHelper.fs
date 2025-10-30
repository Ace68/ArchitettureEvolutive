namespace BrewUp.Warehouse.Domain.FSharp

open System
open BrewUp.Warehouse.SharedKernel.FSharp

module WarehouseDomainHelper =
    
    let createStockQuantity (value: decimal) =
        if value < 0m then
            failwith "Stock quantity must be non-negative"
        else
            StockQuantity value
    
    let createUnitOfMeasure (value: string) =
        if String.IsNullOrWhiteSpace(value) then
            failwith "Unit of measure cannot be empty"
        else
            UnitOfMeasure (value.Trim())
    
    let createProductDescription (value: string) =
        if String.IsNullOrWhiteSpace(value) then
            failwith "Product description cannot be empty"
        else
            ProductDescription (value.Trim())
    
    let createStockItemId () =
        StockItemId (Guid.NewGuid())
    
    let createWarehouseLocationId () =
        WarehouseLocationId (Guid.NewGuid())
    
    let createBeerId () =
        BeerId (Guid.NewGuid())