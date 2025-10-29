using BrewUp.Warehouse.SharedKernel.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouse.Domain.CommandHandlers;

public sealed class PrepareSalesOrderCommandHandler(IRepository repository, ILoggerFactory loggerFactory) 
    : CommandHandlerBaseAsync<PrepareSalesOrder>(repository, loggerFactory)
{
    public override Task HandleAsync(PrepareSalesOrder command, CancellationToken cancellationToken = new ())
    {
        return Task.CompletedTask;
    }
}