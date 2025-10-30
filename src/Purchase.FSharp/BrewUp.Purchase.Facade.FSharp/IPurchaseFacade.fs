namespace BrewUp.Purchase.Facade.FSharp

open System.Threading.Tasks

type IPurchaseFacade =
    abstract GetPurchaseHealthAsync: unit -> Task<string>