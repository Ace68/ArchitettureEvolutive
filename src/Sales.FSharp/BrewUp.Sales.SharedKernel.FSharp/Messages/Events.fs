namespace BrewUp.Sales.SharedKernel.FSharp.Messages

open System
open Muflone.Messages.Events
open BrewUp.Sales.SharedKernel.FSharp.CustomTypes

// Simplified events for Sales domain (no handlers as requested)
type SalesOrderCreated = {
    SalesOrderId: string
    SalesOrderNumber: string
    CustomerId: string
    CustomerName: string
    CreatedAt: DateTime
}

type SalesOrderClosed = {
    SalesOrderId: string
    ClosedAt: DateTime
}

// Integration events for inter-module communication
type SalesOrderReadyForProcessing = {
    SalesOrderId: string
    SalesOrderNumber: string
    ProcessingRequested: DateTime
}

type SalesOrderProductsPrepared = {
    SalesOrderId: string
    SalesOrderNumber: string
    ProductsPrepared: DateTime
}