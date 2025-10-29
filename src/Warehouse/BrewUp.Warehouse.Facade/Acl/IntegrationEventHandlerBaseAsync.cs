using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.Facade.Acl;

public abstract class IntegrationEventHandlerBaseAsync<TEvent>(ILoggerFactory loggerFactory) : 
    IIntegrationEventHandlerAsync<TEvent> where TEvent : IntegrationEvent
{
    protected readonly ILogger Logger = loggerFactory.CreateLogger<IntegrationEventHandlerBaseAsync<TEvent>>();

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

    ~IntegrationEventHandlerBaseAsync()
    {
        Dispose(false);
    }

    #endregion
}