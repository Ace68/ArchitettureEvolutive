using System.Linq.Expressions;
using BrewUp.Sales.Entities.Entities;
using BrewUp.Sales.Infrastructure;
using BrewUp.Shared.Exceptions;
using BrewUp.Shared.ReadModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.Queries;

public sealed class SalesOrderQuery(SalesContext salesContext,
    ILoggerFactory loggerFactory) : IQueries<SalesOrder>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<SalesOrderQuery>();
    
    public async Task<SalesOrder> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            var queryable = salesContext.Set<SalesOrder>()
                .Include(c => c.SalesOrderRows)
                .Where(a => a.Id.Equals(id));
            var result = await queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return result ??
                   throw new EntityNotFoundException(
                       $"Entity of type {nameof(SalesOrder)} with id {id} not found.");
        }
        catch (Exception ex)
        {
            UtilitiesService.LogError(ex, _logger);
            throw;
        }
    }

    public async Task<PagedResult<SalesOrder>> GetByFilterAsync(Expression<Func<SalesOrder, bool>>? query, int page, int pageSize, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if (--page < 0)
            page = 0;

        try
        {
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
        }
        catch (Exception ex)
        {
            UtilitiesService.LogError(ex, _logger);
            throw;
        }
    }
}