using BrewUp.Warehouse.SharedKernel.CustomTypes;
using BrewUp.Warehouse.SharedKernel.Messages.Commands;
using BrewUp.Warehouse.SharedKernel.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouse.Facade.Acl;

public sealed class SalesOrderReadyForProcessingEventHandler(
    IServiceBus serviceBus,
    ILoggerFactory loggerFactory) 
    : IntegrationEventHandlerBaseAsync<SalesOrderReadyForProcessing>(loggerFactory)
{
    public override async Task HandleAsync(SalesOrderReadyForProcessing @event, CancellationToken cancellationToken = new ())
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var correlationId = GetCorrelationIdFromEvent(@event);
        
        PrepareSalesOrder command = new (
            new SalesOrderId(@event.AggregateId.Value),
            @event.SalesOrderNumber,
            @event.SalesOrderDeliveryDate,
            @event.Rows, correlationId);
        
        await serviceBus.SendAsync(command, cancellationToken);
    }
}