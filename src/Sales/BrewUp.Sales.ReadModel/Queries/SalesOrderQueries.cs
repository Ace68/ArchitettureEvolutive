using System.Linq.Expressions;
using BrewUp.Sales.Entities.Entities;
using BrewUp.Sales.Infrastructure;
using BrewUp.Shared.ReadModel;
using Microsoft.EntityFrameworkCore;

namespace BrewUp.Sales.ReadModel.Queries;

internal static class SalesOrderQueries
{
    internal static readonly Func<SalesContext, Expression<Func<SalesOrder, bool>>?, int, int, Task<PagedResult<SalesOrder>>> GetSalesOrderByFilter =
        async (salesContext, query, page, pageSize) =>
        {
            if (--page < 0)
                page = 0;

            CancellationToken cancellationToken = CancellationToken.None;

            var queryable = query != null
                ? salesContext.Set<SalesOrder>()
                    .Include(c => c.SalesOrderRows)
                    .Where(query)
                : salesContext.Set<SalesOrder>()
                    .Include(c => c.SalesOrderRows);

            var count = await queryable.CountAsync(cancellationToken: cancellationToken);
            var results = await queryable.Skip(page * pageSize).Take(pageSize)
                .ToListAsync(cancellationToken: cancellationToken);

            return new PagedResult<SalesOrder>(results, page, pageSize, count);
        };
}