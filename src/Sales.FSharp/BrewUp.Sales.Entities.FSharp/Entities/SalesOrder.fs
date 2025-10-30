namespace BrewUp.Sales.Entities.FSharp.Entities

open System
open BrewUp.Shared.FSharp.Domain
open BrewUp.Shared.FSharp.ExternalContracts
open BrewUp.Sales.SharedKernel.FSharp.CustomTypes
open BrewUp.Sales.SharedKernel.FSharp.Messages

type SalesOrder = {
    Id: string
    SalesOrderNumber: string
    SalesOrderDate: DateTime
    CustomerId: string
    CustomerName: string
    SalesOrderDeliveryDate: DateTime
    SalesOrderRows: SalesOrderRow list
    Status: string
    UncommittedEvents: obj list // Simplified events storage
}

module SalesOrder =
    let create (salesOrderId: SalesOrderId) (salesOrderNumber: SalesOrderNumber) (salesOrderDate: SalesOrderDate)
               (customerId: CustomerId) (customerName: CustomerName) (deliveryDate: SalesOrderDeliveryDate)
               (rows: SalesOrderRowJson array) (correlationId: Guid) =
        
        let orderRows = 
            rows
            |> Array.map (fun row ->
                SalesOrderRow.create 
                    salesOrderId
                    (ProductId.From row.ProductId)
                    (ProductName.Create row.ProductName)
                    row.Quantity
                    row.Price)
            |> Array.toList
        
        let salesOrder = {
            Id = salesOrderId.Value
            SalesOrderNumber = salesOrderNumber.Value
            SalesOrderDate = salesOrderDate.Value
            CustomerId = customerId.Value
            CustomerName = customerName.Value
            SalesOrderDeliveryDate = deliveryDate.Value
            SalesOrderRows = orderRows
            Status = "Created"
            UncommittedEvents = []
        }
        
        // Create domain event (simplified)
        let event = {
            SalesOrderId = salesOrder.Id
            SalesOrderNumber = salesOrder.SalesOrderNumber
            CustomerId = salesOrder.CustomerId
            CustomerName = salesOrder.CustomerName
            CreatedAt = DateTime.UtcNow
        }
        
        { salesOrder with UncommittedEvents = [event] }
    
    let close (salesOrder: SalesOrder) =
        let closedEvent = {
            SalesOrderId = salesOrder.Id
            ClosedAt = DateTime.UtcNow
        }
        
        { salesOrder with 
            Status = "Closed"
            UncommittedEvents = salesOrder.UncommittedEvents @ [closedEvent] }
    
    let markReadyForProcessing (salesOrder: SalesOrder) =
        let readyEvent = {
            SalesOrderId = salesOrder.Id
            SalesOrderNumber = salesOrder.SalesOrderNumber
            ProcessingRequested = DateTime.UtcNow
        }
        
        { salesOrder with 
            Status = "ReadyForProcessing"
            UncommittedEvents = salesOrder.UncommittedEvents @ [readyEvent] }