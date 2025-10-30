namespace BrewUp.Sales.SharedKernel.FSharp.CustomTypes

open System

// Value types for Sales domain
type CustomerId = 
    | CustomerId of string
    
    static member Create(value: string) =
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "CustomerId cannot be null or whitespace"
        CustomerId value
    
    member this.Value = 
        match this with 
        | CustomerId v -> v

type CustomerName = 
    | CustomerName of string
    
    static member Create(value: string) =
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "CustomerName cannot be null or whitespace"
        CustomerName value
    
    member this.Value = 
        match this with 
        | CustomerName v -> v

type SalesOrderNumber = 
    | SalesOrderNumber of string
    
    static member Create(value: string) =
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "SalesOrderNumber cannot be null or whitespace"
        SalesOrderNumber value
    
    member this.Value = 
        match this with 
        | SalesOrderNumber v -> v

type ProductName = 
    | ProductName of string
    
    static member Create(value: string) =
        if String.IsNullOrWhiteSpace(value) then
            invalidArg "value" "ProductName cannot be null or whitespace"
        ProductName value
    
    member this.Value = 
        match this with 
        | ProductName v -> v

type SalesOrderDate = 
    | SalesOrderDate of DateTime
    
    static member Create(value: DateTime) =
        if value = DateTime.MinValue then
            invalidArg "value" "SalesOrderDate cannot be MinValue"
        SalesOrderDate value
    
    member this.Value = 
        match this with 
        | SalesOrderDate v -> v

type SalesOrderDeliveryDate = 
    | SalesOrderDeliveryDate of DateTime
    
    static member Create(value: DateTime) =
        if value = DateTime.MinValue then
            invalidArg "value" "SalesOrderDeliveryDate cannot be MinValue"
        SalesOrderDeliveryDate value
    
    member this.Value = 
        match this with 
        | SalesOrderDeliveryDate v -> v