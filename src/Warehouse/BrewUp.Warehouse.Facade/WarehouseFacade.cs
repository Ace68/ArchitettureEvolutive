using BrewUp.Shared.ExternalContracts;

namespace BrewUp.Warehouse.Facade;

internal class WarehouseFacade : IWarehouseFacade
{
    public Task GetAvailableStockAsync(IEnumerable<SalesOrderRowJson> rows, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}