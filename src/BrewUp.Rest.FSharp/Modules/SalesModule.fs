namespace BrewUp.Rest.FSharp.Modules

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Giraffe

type SalesModule() =
    interface IModule with
        member _.IsEnabled = true
        member _.Order = 0
        
        member _.Register(builder: WebApplicationBuilder) =
            BrewUp.Sales.Facade.FSharp.SalesFacadeHelper.addSalesFacade builder.Services builder.Configuration |> ignore
            builder.Services
            
        member _.Configure(app: WebApplication) =
            app