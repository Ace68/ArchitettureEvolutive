using BrewUp.Shared.ExternalContracts;

namespace BrewUp.Sales.Domain;

public interface ISalesDomainService
{
    Task<string> CreateSalesOrderAsync(CreateSalesOrderJson body, CancellationToken cancellationToken);
}