namespace BrewUp.Sales.Facade.FSharp.Endpoints

open Microsoft.AspNetCore.Http
open Giraffe
open BrewUp.Shared.FSharp.ExternalContracts
open BrewUp.Sales.Facade.FSharp

module SalesEndpoints =
    
    let createSalesOrder: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! createOrder = ctx.BindJsonAsync<CreateSalesOrderJson>()
                let salesFacade = ctx.GetService<ISalesFacade>()
                let! orderId = salesFacade.CreateSalesOrderAsync(createOrder)
                
                return! json {| Id = orderId; Message = "Sales order created successfully" |} next ctx
            }
    
    let getSalesOrders: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let page = ctx.TryGetQueryStringValue("page") |> Option.defaultValue "1" |> int
                let pageSize = ctx.TryGetQueryStringValue("pageSize") |> Option.defaultValue "10" |> int
                
                let salesFacade = ctx.GetService<ISalesFacade>()
                let! result = salesFacade.GetSalesOrdersAsync(page, pageSize)
                
                return! json result next ctx
            }
    
    let salesRoutes: HttpHandler =
        subRoute "/v1/sales" (
            choose [
                route "/" >=> choose [
                    POST >=> createSalesOrder
                    GET >=> getSalesOrders
                ]
            ]
        )