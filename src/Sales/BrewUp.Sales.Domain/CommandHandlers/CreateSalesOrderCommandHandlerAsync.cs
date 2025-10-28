using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Messages.Commands;
using BrewUp.Shared.Domain;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.Domain.CommandHandlers;

public sealed class CreateSalesOrderCommandHandlerAsync(IBrewUpRepository<Entities.Entities.SalesOrder> repository,
    ILoggerFactory loggerFactory) : CommandHandlerBaseAsync<CreateSalesOrder>(repository, loggerFactory)
{
    public override async Task HandleAsync(CreateSalesOrder command, CancellationToken cancellationToken = new ())
    {
        var aggregate = Entities.Entities.SalesOrder.Create(new SalesOrderId(command.AggregateId.Value),
            command.SalesOrderNumber,
            command.SalesOrderDate,
            command.CustomerId,
            command.CustomerName,
            command.SalesOrderDeliveryDate,
            command.Rows);
        
        await repository.AddAsync(aggregate, cancellationToken);
    }
}