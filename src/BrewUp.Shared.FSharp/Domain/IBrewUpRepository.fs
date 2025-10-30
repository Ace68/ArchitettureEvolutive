namespace BrewUp.Shared.FSharp.Domain

open System.Collections.Generic
open System.Threading
open System.Threading.Tasks
open Muflone.Messages.Events
open Muflone.Persistence

type IBrewUpRepository<'T when 'T :> BrewUpAggregateRoot> =
    inherit IRepository
    
    abstract GetByIdAsync: id: string * cancellationToken: CancellationToken -> Task<'T>
    abstract AddAsync: entity: 'T * cancellationToken: CancellationToken -> Task<unit>
    abstract UpdateAsync: entity: 'T * cancellationToken: CancellationToken -> Task<unit>
    abstract DeleteAsync: entity: 'T * cancellationToken: CancellationToken -> Task<unit>
    abstract PublishAggregateEventsAsync: entity: 'T * cancellationToken: CancellationToken -> Task<unit>
    abstract GetUncommittedEvents: unit -> IEnumerable<DomainEvent>