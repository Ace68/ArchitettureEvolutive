using BrewUp.Warehouse.SharedKernel.CustomTypes;
using BrewUp.Warehouse.SharedKernel.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BrewUp.Warehouse.ReadModel.EventHandlers;

public class AvailabilityUpdatedEventHandler(IEventBus eventBus,
    ILoggerFactory loggerFactory) : DomainEventHandlerBaseAsync<AvailabilityUpdated>(loggerFactory)
{
    public override async Task HandleAsync(AvailabilityUpdated @event, CancellationToken cancellationToken = new ())
    {
        Guid correlationId = GetCorrelationIdFromEvent(@event);
        SalesOrderProductsPrepared integrationEvent = new((SalesOrderId) @event.AggregateId, @event.SalesOrderNumber,
            @event.Rows, correlationId);
        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}