using BrewUp.Sales.Domain;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.Facade;

internal class SalesFacade(ISalesDomainService salesDomainService,
  ISalesOrderService salesOrderService) : ISalesFacade
{
    public async Task<string> CreateSalesOrderAsync(CreateSalesOrderJson createSalesOrderJson, CancellationToken cancellationToken)
    {
      cancellationToken.ThrowIfCancellationRequested();
      return await salesDomainService.CreateSalesOrderAsync(createSalesOrderJson, cancellationToken);
  }

    public async Task<PagedResult<SalesOrderJson>> GetSalesOrdersAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
      cancellationToken.ThrowIfCancellationRequested();

      return await salesOrderService.GetSalesOrdersAsync(page, pageSize, cancellationToken);
  }
}