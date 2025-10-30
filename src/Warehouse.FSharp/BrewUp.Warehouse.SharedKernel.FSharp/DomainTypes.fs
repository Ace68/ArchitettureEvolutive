namespace BrewUp.Warehouse.SharedKernel.FSharp

open System

type StockQuantity = StockQuantity of decimal
    with
        member this.Value = let (StockQuantity value) = this in value

type UnitOfMeasure = UnitOfMeasure of string
    with
        member this.Value = let (UnitOfMeasure value) = this in value

type ProductDescription = ProductDescription of string
    with
        member this.Value = let (ProductDescription value) = this in value

type StockItemId = StockItemId of Guid
    with
        member this.Value = let (StockItemId value) = this in value

type WarehouseLocationId = WarehouseLocationId of Guid
    with
        member this.Value = let (WarehouseLocationId value) = this in value

type BeerId = BeerId of Guid
    with
        member this.Value = let (BeerId value) = this in value