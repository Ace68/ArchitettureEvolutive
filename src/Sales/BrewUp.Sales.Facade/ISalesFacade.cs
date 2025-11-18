using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.Facade;

public interface ISalesFacade
{
    Task<string> CreateSalesOrderAsync(CreateSalesOrderJson body, CancellationToken cancellationToken);
    Task<PagedResult<SalesOrderJson>> GetSalesOrdersAsync(int page, int pageSize, CancellationToken cancellationToken);
}