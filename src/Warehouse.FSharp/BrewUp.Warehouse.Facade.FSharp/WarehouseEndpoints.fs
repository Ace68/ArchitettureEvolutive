namespace BrewUp.Warehouse.Facade.FSharp

open Microsoft.AspNetCore.Http
open Giraffe
open System.Threading.Tasks
open BrewUp.Rest.FSharp

module WarehouseEndpoints =
    
    let private healthHandler: HttpHandler =
        fun next ctx ->
            let warehouseFacade = ctx.GetService<IWarehouseFacade>()
            task {
                let! isHealthy = warehouseFacade.GetHealthAsync()
                return! json { Status = if isHealthy then "Healthy" else "Unhealthy" } next ctx
            }
    
    let routes: HttpHandler =
        subRoute "/v1/warehouse" (
            choose [
                GET >=> route "/health" >=> healthHandler
            ]
        )
    
    type WarehouseModule() =
        interface IModule with
            member _.RegisterRoutes() = routes