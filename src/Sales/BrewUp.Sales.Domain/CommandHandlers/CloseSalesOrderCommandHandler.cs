using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Messages.Commands;
using BrewUp.Shared.Domain;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.Domain.CommandHandlers;

public class CloseSalesOrderCommandHandler(IBrewUpRepository<Entities.Entities.SalesOrder> repository,
    ILoggerFactory loggerFactory) : CommandHandlerBaseAsync<CloseSalesOrder>(repository, loggerFactory)
{
    public override async Task HandleAsync(CloseSalesOrder command, CancellationToken cancellationToken = new ())
    {
        Entities.Entities.SalesOrder aggregate = await repository.GetByIdAsync(command.AggregateId.Value, cancellationToken);
        aggregate.CloseSalesOrder((SalesOrderId) command.AggregateId, command.SalesOrderDeliveryDate, command.MessageId);
        await repository.UpdateAsync(aggregate, cancellationToken);
    }
}