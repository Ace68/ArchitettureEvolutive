open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Giraffe
open BrewUp.Rest.FSharp.Modules.ModuleExtensions
open BrewUp.Sales.Facade.FSharp.Endpoints
open BrewUp.Purchase.Facade.FSharp
open BrewUp.Warehouse.Facade.FSharp

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    
    builder.Services.AddGiraffe() |> ignore
    
    let configuredBuilder = registerModules builder
    
    let app = configuredBuilder.Build()
    
    let configuredApp = configureModules app
    
    let webApp = 
        choose [
            SalesEndpoints.salesRoutes
            route "/" >=> text "BrewUp F# API is running!"
            route "/health" >=> text "Healthy"
        ]
    
    configuredApp.UseGiraffe(webApp)
    
    configuredApp.Run()
    0

