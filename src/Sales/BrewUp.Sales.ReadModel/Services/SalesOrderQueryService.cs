using System.Linq.Expressions;
using BrewUp.Sales.Entities.Entities;
using BrewUp.Sales.Infrastructure;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using Serilog.Core;

namespace BrewUp.Sales.ReadModel.Services;

internal delegate Task<PagedResult<SalesOrder>> GetSalesOrderByFilter(
    Expression<Func<SalesOrder, bool>>? query,
    int page,
    int pageSize);

internal static class SalesOrderQueryService
{
    internal static GetSalesOrders GetSalesOrders(Logger logger, Func<SalesContext, Expression<Func<SalesOrder, bool>>?, int, int, Task<PagedResult<SalesOrder>>> query)
    {
        return async (salesContext, page, pageSize) =>
        {
            var pagedResult = await query(salesContext, null, page, pageSize);
            var jsonResults = pagedResult.Results.Select(_ => new SalesOrderJson()).ToList();
            return new PagedResult<SalesOrderJson>(jsonResults, pagedResult.Page, pagedResult.PageSize, pagedResult.TotalRecords);
        };
    }
}