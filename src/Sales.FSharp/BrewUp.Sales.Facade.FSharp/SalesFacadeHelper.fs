namespace BrewUp.Sales.Facade.FSharp

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Configuration
open BrewUp.Sales.Domain.FSharp

module SalesFacadeHelper =
    
    let addSalesFacade (services: IServiceCollection) (configuration: IConfiguration) =
        services.AddScoped<ISalesFacade, SalesFacade>() |> ignore
        
        SalesDomainHelper.addSalesDomain services |> ignore
        
        services