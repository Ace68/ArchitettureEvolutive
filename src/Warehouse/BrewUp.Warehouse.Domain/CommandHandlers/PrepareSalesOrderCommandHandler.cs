using BrewUp.Shared.Domain;
using BrewUp.Warehouse.Entities.Entities;
using BrewUp.Warehouse.SharedKernel.CustomTypes;
using BrewUp.Warehouse.SharedKernel.Messages.Commands;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.Domain.CommandHandlers;

public sealed class PrepareSalesOrderCommandHandler(IBrewUpRepository<Product> repository, 
    ILoggerFactory loggerFactory) : CommandHandlerBaseAsync<PrepareSalesOrder>(repository, loggerFactory)
{
    public override async Task HandleAsync(PrepareSalesOrder command, CancellationToken cancellationToken = new ())
    {
        Product aggregate = await repository.GetByIdAsync(command.AggregateId.Value, cancellationToken);
        aggregate.PrepareSalesOrder((SalesOrderId) command.AggregateId, command.SalesOrderNumber, command.Rows,
            command.MessageId);
        await repository.UpdateAsync(aggregate, cancellationToken);
    }
}