using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BrewUp.Sales.ReadModel.EventHandlers;

public sealed class SalesOrderCreatedEventHandler(
    IEventBus eventBus,
    ILoggerFactory loggerFactory) 
    : DomainEventHandlerBaseAsync<SalesOrderCreated>(loggerFactory)
{
    public override async Task HandleAsync(SalesOrderCreated @event, CancellationToken cancellationToken = new ())
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var correlationId = GetCorrelationIdFromEvent(@event);
        
        SalesOrderReadyForProcessing integrationEvent = new (
            new SalesOrderId(@event.AggregateId.Value),
            @event.SalesOrderNumber,
            @event.SalesOrderDeliveryDate,
            @event.Rows,
            correlationId);
        
        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}