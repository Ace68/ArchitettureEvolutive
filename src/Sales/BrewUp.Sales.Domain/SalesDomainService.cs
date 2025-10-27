using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Messages.Commands;
using BrewUp.Shared.ExternalContracts;
using Muflone.Persistence;

namespace BrewUp.Sales.Domain;

internal class SalesDomainService(IServiceBus serviceBus) : ISalesDomainService
{
    public async Task<string> CreateSalesOrderAsync(CreateSalesOrderJson body, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var salesOrderId = Guid.NewGuid().ToString();
        
        CreateSalesOrder command = new CreateSalesOrder(new SalesOrderId(salesOrderId),
            new SalesOrderNumber(body.OrderNumber),
            new SalesOrderDate(body.OrderDate),
            new CustomerId(body.CustomerId),
            new CustomerName(body.CustomerName),
            new SalesOrderDeliveryDate(body.DeliveryDate),
            body.Rows);

        await serviceBus.SendAsync(command, cancellationToken);
        
        return salesOrderId;
    }
}