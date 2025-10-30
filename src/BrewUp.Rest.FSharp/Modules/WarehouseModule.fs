namespace BrewUp.Rest.FSharp.Modules

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Giraffe
open BrewUp.Warehouse.Facade.FSharp

type WarehouseModule() =
    interface IModule with
        member _.IsEnabled = true
        member _.Order = 0
        
        member _.Register(builder: WebApplicationBuilder) =
            builder.Services.AddScoped<IWarehouseFacade, WarehouseFacadeHelper>() |> ignore
            builder.Services
            
        member _.Configure(app: WebApplication) =
            let warehouseModule = WarehouseEndpoints.WarehouseModule()
            let routes = (warehouseModule :> IModule).RegisterRoutes()
            app.UseGiraffe(routes)
            app