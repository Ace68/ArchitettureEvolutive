using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.ReadModel.Services;

public interface ISalesOrderService
{
    Task<PagedResult<SalesOrderJson>> GetSalesOrdersAsync(int page, int pageSize, CancellationToken cancellationToken);
}