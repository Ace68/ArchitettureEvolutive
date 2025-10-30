namespace BrewUp.Purchase.SharedKernel.FSharp

open System
open Muflone.Core

// Purchase domain types - ready for future expansion
type PurchaseOrderId private (value: string) =
    inherit DomainId(value)
    
    static member New() = PurchaseOrderId(Guid.NewGuid().ToString())
    static member From(value: string) = 
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "PurchaseOrderId cannot be null or whitespace"
        PurchaseOrderId(value)

type SupplierId = 
    | SupplierId of string
    
    static member Create(value: string) =
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "SupplierId cannot be null or whitespace"
        SupplierId value
    
    member this.Value = 
        match this with 
        | SupplierId v -> v

type SupplierName = 
    | SupplierName of string
    
    static member Create(value: string) =
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "SupplierName cannot be null or whitespace"
        SupplierName value
    
    member this.Value = 
        match this with 
        | SupplierName v -> v

type PurchaseOrderNumber = 
    | PurchaseOrderNumber of string
    
    static member Create(value: string) =
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "PurchaseOrderNumber cannot be null or whitespace"
        PurchaseOrderNumber value
    
    member this.Value = 
        match this with 
        | PurchaseOrderNumber v -> v

// Purchase events (simplified, no handlers)
type PurchaseOrderCreated = {
    PurchaseOrderId: string
    PurchaseOrderNumber: string
    SupplierId: string
    SupplierName: string
    CreatedAt: DateTime
}

type PurchaseOrderReceived = {
    PurchaseOrderId: string
    ReceivedAt: DateTime
}