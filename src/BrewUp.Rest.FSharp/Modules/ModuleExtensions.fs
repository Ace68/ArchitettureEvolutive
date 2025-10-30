namespace BrewUp.Rest.FSharp.Modules

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open BrewUp.Purchase.Facade.FSharp
open BrewUp.Warehouse.Facade.FSharp

module ModuleExtensions =
    
    let private getModules () = [
        OpenApiModule() :> IModule
        SalesModule() :> IModule
        PurchaseModule() :> IModule
        WarehouseModule() :> IModule
    ]
    
    let registerModules (builder: WebApplicationBuilder) =
        let modules = 
            getModules()
            |> List.filter (fun m -> m.IsEnabled)
            |> List.sortBy (fun m -> m.Order)
        
        modules
        |> List.fold (fun services m -> m.Register(builder)) builder.Services
        |> ignore
        
        builder
    
    let configureModules (app: WebApplication) =
        let modules = 
            getModules()
            |> List.filter (fun m -> m.IsEnabled)
            |> List.sortBy (fun m -> m.Order)
        
        modules
        |> List.fold (fun acc m -> m.Configure(acc)) app