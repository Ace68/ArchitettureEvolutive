using BrewUp.Sales.Facade;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Warehouse.Facade;

namespace BrewUp.Mediator.Facade;

internal class MediatorFacade(ISalesFacade salesFacade,
  IWarehouseFacade warehouseFacade) : IMediatorFacade
{
    public async Task<string> CreateSalesOrderAsync(CreateSalesOrderJson body, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var availableStock = await warehouseFacade.GetAvailableStockAsync(body.Rows, cancellationToken);

        return await salesFacade.CreateSalesOrderAsync(body, cancellationToken);
    }
}
