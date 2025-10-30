namespace BrewUp.Warehouse.Facade.FSharp

open System.Threading.Tasks
open Microsoft.Extensions.Logging

type WarehouseFacadeHelper(logger: ILogger<WarehouseFacadeHelper>) =
    
    interface IWarehouseFacade with
        member _.GetHealthAsync() =
            logger.LogInformation("Warehouse module is healthy")
            Task.FromResult(true)