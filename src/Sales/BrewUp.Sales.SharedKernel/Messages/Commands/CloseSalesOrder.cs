using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.ExternalContracts;
using Muflone.Messages.Commands;

namespace BrewUp.Sales.SharedKernel.Messages.Commands;

public sealed class CloseSalesOrder(
    SalesOrderId aggregateId,
    SalesOrderNumber salesOrderNumber,
    SalesOrderDeliveryDate salesOrderDeliveryDate,
    IEnumerable<SalesOrderRowJson> rows,
    Guid correlationId) : Command(aggregateId, correlationId)
{
    public SalesOrderNumber SalesOrderNumber { get; private set; } = salesOrderNumber;
    
    public SalesOrderDeliveryDate SalesOrderDeliveryDate { get; private set; } = salesOrderDeliveryDate;
    public IEnumerable<SalesOrderRowJson> Rows { get; private set; } = rows;
}