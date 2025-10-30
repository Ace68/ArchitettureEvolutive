namespace BrewUp.Sales.Facade.FSharp

open System
open System.Threading.Tasks
open BrewUp.Shared.FSharp.ExternalContracts
open BrewUp.Shared.FSharp.ReadModel
open BrewUp.Sales.Entities.FSharp.Entities
open BrewUp.Sales.SharedKernel.FSharp.CustomTypes

type SalesFacade() =
    interface ISalesFacade with
        member _.CreateSalesOrderAsync(createSalesOrder: CreateSalesOrderJson) : Task<string> =
            task {
                // Create domain objects
                let salesOrderId = SalesOrderId.New()
                let salesOrderNumber = SalesOrderNumber.Create(createSalesOrder.OrderNumber)
                let salesOrderDate = SalesOrderDate.Create(createSalesOrder.OrderDate)
                let customerId = CustomerId.Create(createSalesOrder.CustomerId)
                let customerName = CustomerName.Create(createSalesOrder.CustomerName)
                let deliveryDate = SalesOrderDeliveryDate.Create(createSalesOrder.DeliveryDate)
                
                // Create sales order
                let salesOrder = SalesOrder.create salesOrderId salesOrderNumber salesOrderDate 
                                   customerId customerName deliveryDate createSalesOrder.Rows (Guid.NewGuid())
                
                // In a real implementation, save to repository here
                
                return salesOrder.Id
            }
        
        member _.GetSalesOrdersAsync(page: int, pageSize: int) : Task<PagedResult<SalesOrderJson>> =
            task {
                // In a real implementation, fetch from repository/read model
                let mockResults = [||]
                
                return {
                    Results = mockResults
                    Page = page
                    PageSize = pageSize
                    TotalRecords = 0
                }
            }