namespace BrewUp.Purchase.Facade.FSharp

open System.Threading.Tasks

type PurchaseFacade() =
    interface IPurchaseFacade with
        member _.GetPurchaseHealthAsync() : Task<string> =
            Task.FromResult("Purchase module is healthy!")