using BrewUp.Sales.ReadModel;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using Serilog.Core;

namespace BrewUp.Sales.Facade;

public delegate Task<PagedResult<SalesOrderJson>> GetSalesOrders(int page, int pageSize);
  
public record SalesCompositionRoot(
    ReadModel.GetSalesOrders GetSalesOrders)
{
    public static SalesCompositionRoot Build(Logger logger)
    {
        var salesReadModelComposition = SalesReadModelComposition.Build(logger);
        return new SalesCompositionRoot(
            GetSalesOrders: 
                salesReadModelComposition.GetSalesOrders);
    }
}