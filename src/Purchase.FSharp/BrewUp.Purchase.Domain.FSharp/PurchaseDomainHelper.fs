namespace BrewUp.Purchase.Domain.FSharp

open Microsoft.Extensions.DependencyInjection

module PurchaseDomainHelper =
    
    let addPurchaseDomain (services: IServiceCollection) =
        // Register domain services here when needed
        services