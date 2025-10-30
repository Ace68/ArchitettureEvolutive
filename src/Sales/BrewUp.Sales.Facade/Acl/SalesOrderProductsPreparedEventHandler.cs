using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Messages.Commands;
using BrewUp.Sales.SharedKernel.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Sales.Facade.Acl;

public sealed class SalesOrderProductsPreparedEventHandler(IServiceBus serviceBus,
    ILoggerFactory loggerFactory) : IntegrationEventHandlerBaseAsync<SalesOrderProductsPrepared>(loggerFactory)
{
    public override async Task HandleAsync(SalesOrderProductsPrepared @event, CancellationToken cancellationToken = new ())
    {
        Guid correlationId = GetCorrelationIdFromEvent(@event);
        
        CloseSalesOrder command = new CloseSalesOrder((SalesOrderId) @event.AggregateId, @event.SalesOrderNumber, 
            new SalesOrderDeliveryDate(DateTime.UtcNow), @event.Rows, correlationId);
        await serviceBus.SendAsync(command, cancellationToken);
    }
}