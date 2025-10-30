namespace BrewUp.Infrastructure.FSharp

open Microsoft.Extensions.DependencyInjection

module InfrastructureHelper =
    
    let addBrewUpInfrastructure (services: IServiceCollection) =
        // Register shared infrastructure services here when needed
        services

type EventStoreSettings() =
    // Configuration settings for EventStore will be added later
    member val ConnectionString = "" with get, set
    member val DatabaseName = "" with get, set