namespace BrewUp.Purchase.Facade.FSharp.Endpoints

open Microsoft.AspNetCore.Http
open Giraffe
open BrewUp.Purchase.Facade.FSharp

module PurchaseEndpoints =
    
    let getPurchaseHealth: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let purchaseFacade = ctx.GetService<IPurchaseFacade>()
                let! healthMessage = purchaseFacade.GetPurchaseHealthAsync()
                
                return! json {| Status = "OK"; Message = healthMessage |} next ctx
            }
    
    let purchaseRoutes: HttpHandler =
        subRoute "/v1/purchase" (
            choose [
                route "/health" >=> GET >=> getPurchaseHealth
                route "/" >=> GET >=> json {| Message = "Purchase API endpoints - Ready for future expansion" |}
            ]
        )