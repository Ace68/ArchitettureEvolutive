using BrewUp.Shared.ExternalContracts;

namespace BrewUp.Mediator.Facade;

public interface IMediatorFacade
{
  Task<string> CreateSalesOrderAsync(CreateSalesOrderJson body, CancellationToken cancellationToken);
}
