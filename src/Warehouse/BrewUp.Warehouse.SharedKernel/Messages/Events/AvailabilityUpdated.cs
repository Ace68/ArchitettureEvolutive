using BrewUp.Shared.ExternalContracts;
using BrewUp.Warehouse.SharedKernel.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.SharedKernel.Messages.Events;

public sealed class AvailabilityUpdated(SalesOrderId aggregateId, SalesOrderNumber salesOrderNumber,
    IEnumerable<SalesOrderRowJson> rows, Guid correlationId) : DomainEvent(aggregateId, correlationId)
{
    public SalesOrderNumber SalesOrderNumber { get; private set; } = salesOrderNumber;
    public IEnumerable<SalesOrderRowJson> Rows { get; private set; } = rows;
}