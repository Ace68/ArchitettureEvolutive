namespace BrewUp.Shared.FSharp.Domain

open System.Collections.Generic
open BrewUp.Shared.FSharp.Domain
open Muflone.Messages.Events

[<AbstractClass>]
type BrewUpAggregateRoot() =
    inherit DtoBase()
    
    let mutable _uncommittedEvents = ResizeArray<DomainEvent>()
    
    member this.RaiseEvent(event: DomainEvent) =
        _uncommittedEvents.Add(event)
    
    member this.GetUncommittedEvents() : ICollection<DomainEvent> =
        upcast _uncommittedEvents
    
    member this.ClearUncommittedEvents() =
        _uncommittedEvents.Clear()