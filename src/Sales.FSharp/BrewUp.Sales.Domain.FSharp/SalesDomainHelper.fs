namespace BrewUp.Sales.Domain.FSharp

open Microsoft.Extensions.DependencyInjection

module SalesDomainHelper =
    
    let addSalesDomain (services: IServiceCollection) =
        // Register domain services here when needed
        services