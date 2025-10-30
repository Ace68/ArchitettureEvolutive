namespace BrewUp.Rest.FSharp.Modules

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Giraffe
open BrewUp.Purchase.Facade.FSharp

type PurchaseModule() =
    interface IModule with
        member _.IsEnabled = true
        member _.Order = 0
        
        member _.Register(builder: WebApplicationBuilder) =
            builder.Services.AddScoped<IPurchaseFacade, PurchaseFacadeHelper>() |> ignore
            builder.Services
            
        member _.Configure(app: WebApplication) =
            let purchaseModule = PurchaseEndpoints.PurchaseModule()
            let routes = (purchaseModule :> IModule).RegisterRoutes()
            app.UseGiraffe(routes)
            app