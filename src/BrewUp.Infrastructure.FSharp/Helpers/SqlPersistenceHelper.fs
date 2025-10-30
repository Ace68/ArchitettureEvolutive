namespace BrewUp.Infrastructure.FSharp.Helpers

open System.Threading.Tasks

module SqlPersistenceHelper =
    
    // Simplified persistence helper - without full event sourcing complexity
    let saveToDatabase connectionString tableName data =
        Task.CompletedTask
        
    let loadFromDatabase connectionString tableName id =
        Task.FromResult(None: 'T option)