using BrewUp.Sales.Domain;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.Facade;

internal class SalesFacade(ISalesDomainService salesDomainService,
    ISalesOrderService salesOrderService) : ISalesFacade
{

    public Task<string> CreateSalesOrderAsync(CreateSalesOrderJson body, CancellationToken cancellationToken) =>
        salesDomainService.CreateSalesOrderAsync(body, cancellationToken);

    public Task<PagedResult<SalesOrderJson>> GetSalesOrdersAsync(int page, int pageSize,
        CancellationToken cancellationToken) =>
        salesOrderService.GetSalesOrdersAsync(page, pageSize, cancellationToken);
}