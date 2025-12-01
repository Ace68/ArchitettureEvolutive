using BrewUp.Sales.Infrastructure;
using BrewUp.Sales.ReadModel.Queries;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using Serilog.Core;

namespace BrewUp.Sales.ReadModel;

public delegate Task<PagedResult<SalesOrderJson>> GetSalesOrders(
    SalesContext salesContext, int page, int pageSize);

public record SalesReadModelComposition(
    GetSalesOrders GetSalesOrders)
{
    public static SalesReadModelComposition Build(Logger logger) =>
        new(
            GetSalesOrders:
            SalesOrderQueryService.GetSalesOrders(logger, SalesOrderQueries.GetSalesOrderByFilter));
}