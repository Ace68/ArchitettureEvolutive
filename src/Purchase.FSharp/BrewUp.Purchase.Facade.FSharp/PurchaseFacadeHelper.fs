namespace BrewUp.Purchase.Facade.FSharp

open System.Threading.Tasks
open Microsoft.Extensions.Logging

type PurchaseFacadeHelper(logger: ILogger<PurchaseFacadeHelper>) =
    
    interface IPurchaseFacade with
        member _.GetPurchaseHealthAsync() =
            logger.LogInformation("Purchase module is healthy")
            Task.FromResult("Healthy")