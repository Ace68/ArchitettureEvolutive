using BrewUp.Shared.ReadModel;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Domain;

public abstract class BrewUpAggregateRoot : DtoBase
{
    private readonly ICollection<DomainEvent> _uncommittedEvents = new LinkedList<DomainEvent>();
    
    protected void RaiseEvent(DomainEvent @event)
    {
        _uncommittedEvents.Add(@event);
    }
    
    public ICollection<DomainEvent> GetUncommittedEvents() => _uncommittedEvents;
    public void ClearUncommittedEvents() => _uncommittedEvents.Clear();
}