using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.ReadModel.EventHandlers;

public abstract class DomainEventHandlerBaseAsync<TEvent>(ILoggerFactory loggerFactory) : IDomainEventHandlerAsync<TEvent> where TEvent : DomainEvent
{
    protected readonly ILogger Logger = loggerFactory.CreateLogger<DomainEventHandlerBaseAsync<TEvent>>();

    public abstract Task HandleAsync(TEvent message, CancellationToken cancellationToken = new());
    
    protected Guid GetCorrelationIdFromEvent(TEvent @event)
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
        
        return correlationId;
    }
    
    #region Dispose

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~DomainEventHandlerBaseAsync()
    {
        Dispose(false);
    }

    #endregion
}