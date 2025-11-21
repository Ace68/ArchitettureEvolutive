using BrewUp.Shared.ExternalContracts;

namespace BrewUp.Warehouse.Facade;

public interface IWarehouseFacade
{
    Task<AvailabilityJson> GetAvailableStockAsync(IEnumerable<SalesOrderRowJson> rows, CancellationToken cancellationToken);
}