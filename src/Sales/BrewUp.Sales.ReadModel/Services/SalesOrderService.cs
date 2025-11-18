using BrewUp.Sales.Entities.Entities;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.ReadModel.Services;

public sealed class SalesOrderService(IQueries<SalesOrder> salesOrderQuery) : ISalesOrderService
{
    public async Task<PagedResult<SalesOrderJson>> GetSalesOrdersAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        PagedResult<SalesOrder> orderResult = await salesOrderQuery.GetByFilterAsync(null, page, pageSize, cancellationToken);

        return orderResult.TotalRecords > 0
            ? new PagedResult<SalesOrderJson>(
                orderResult.Results.Select(s => s.ToJson()),
                orderResult.Page, orderResult.PageSize, orderResult.TotalRecords)
            : new PagedResult<SalesOrderJson>(
                new List<SalesOrderJson>(), 0, 0, 0);
    }
}