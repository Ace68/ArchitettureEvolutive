using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.ExternalContracts;
using Muflone.Messages.Events;

namespace BrewUp.Sales.SharedKernel.Messages.Events;

public sealed class SalesOrderProductsPrepared(SalesOrderId aggregateId, SalesOrderNumber salesOrderNumber,
    IEnumerable<SalesOrderRowJson> rows, Guid correlationId) : IntegrationEvent(aggregateId, correlationId)
{
    public SalesOrderNumber SalesOrderNumber { get; private set; } = salesOrderNumber;
    public IEnumerable<SalesOrderRowJson> Rows { get; private set; } = rows;
}