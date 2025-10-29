using System.Reflection;
using System.Text;
using BrewUp.Infrastructure.Helpers;
using BrewUp.Shared.Domain;
using BrewUp.Shared.Exceptions;
using BrewUp.Warehouse.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Core;
using Muflone.Messages.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BrewUp.Warehouse.Infrastructure.Repository;

public class ProductRepository(WarehouseContext warehouseContext,
    IEventBus eventBus,
    ILoggerFactory loggerFactory) : IBrewUpRepository<Product>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ProductRepository>();
    private IEnumerable<DomainEvent> Published { get; set; } = [];
    private static readonly JsonSerializerSettings SerializerSettings;
    
    static ProductRepository()
    {
        SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            ContractResolver = new PrivateContractResolver()
        };
    }
    
    public async Task<Product> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var queryable = warehouseContext.Set<Product>()
                .Include(c => c.Availabilities)
                .Where(a => a.Id.Equals(id));
            var result = await queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
            return result ?? ConstructAggregate<Product>();
        }
        catch (Exception ex)
        {
            UtilitiesService.LogError(ex, _logger);
            throw;
        }
    }

    public async Task AddAsync(Product entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            if (warehouseContext.Database.ProviderName?.Contains("InMemory") != true)
            {
                await using var transaction = await warehouseContext.Database.BeginTransactionAsync(cancellationToken);
                await AddEntityAsync(entity, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            else
            {
                // Skip transaction for InMemory provider
                await AddEntityAsync(entity, cancellationToken);
            }
            
            await PublishAggregateEventsAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            UtilitiesService.LogError(ex, _logger);
            throw;
        }
    }

    public async Task UpdateAsync(Product entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            if (warehouseContext.Database.ProviderName?.Contains("InMemory") != true)
            {
                await using var transaction = await warehouseContext.Database.BeginTransactionAsync(cancellationToken);
                await UpdateEntityAsync(entity, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            else
            {
                await UpdateEntityAsync(entity, cancellationToken);
            }
        
            await PublishAggregateEventsAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            UtilitiesService.LogError(ex, _logger);
            throw;
        }
    }

    public async Task DeleteAsync(Product entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            if (warehouseContext.Database.ProviderName?.Contains("InMemory") != true)
            {
                await using var transaction = await warehouseContext.Database.BeginTransactionAsync(cancellationToken);
                await DeleteEntityAsync(entity, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            else
            {
                await DeleteEntityAsync(entity, cancellationToken);
            }
            
            await PublishAggregateEventsAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            UtilitiesService.LogError(ex, _logger);
            throw;
        }
    }

    public async Task PublishAggregateEventsAsync(Product entity, CancellationToken cancellationToken)
    {
        IEnumerable<DomainEvent> uncommittedEvents = entity.GetUncommittedEvents().ToList();
        Published = uncommittedEvents;
        
        foreach (var @event in entity.GetUncommittedEvents())
        {
            await eventBus.PublishAsync(@event, cancellationToken);
        }
        entity.ClearUncommittedEvents();
    }

    public IEnumerable<DomainEvent> GetUncommittedEvents()
    {
        return Published;
    }
    
    private async Task AddEntityAsync(Product entity, CancellationToken cancellationToken)
    {
        var dbSet = warehouseContext.Set<Product>();
        await dbSet.AddAsync(entity, cancellationToken);
        await warehouseContext.SaveChangesAsync(cancellationToken);
    }
    
    private async Task UpdateEntityAsync(Product entity, CancellationToken cancellationToken)
    {
        var dbSet = warehouseContext.Set<Product>();
        dbSet.Update(entity);
        await warehouseContext.SaveChangesAsync(cancellationToken);
    }
    
    private async Task DeleteEntityAsync(Product entity, CancellationToken cancellationToken)
    {
        warehouseContext.Set<Product>().Remove(entity);
        await warehouseContext.SaveChangesAsync(cancellationToken);
    }

    // private async Task CommitEventsAsync(SalesOrder entity, Guid commitId,
    //     Action<IDictionary<string, object>> updateHeaders, CancellationToken cancellationToken)
    // {
    //     cancellationToken.ThrowIfCancellationRequested();
    //
    //     var commitHeaders = new Dictionary<string, object>
    //     {
    //         { SqlPersistenceHelper.CommitIdHeader, commitId },
    //         { SqlPersistenceHelper.CommitDateHeader, DateTime.UtcNow},
    //         { SqlPersistenceHelper.AggregateClrTypeHeader, entity.GetType().AssemblyQualifiedName! }
    //     };
    //     
    //     updateHeaders(commitHeaders);
    //
    //     var newEvents = entity.GetUncommittedEvents().Cast<object>().ToList();
    //     var eventsToSave = newEvents.Select(e => commitId.ToEventRecord(entity, e, commitHeaders)).ToList();
    //
    //     try
    //     {
    //         foreach (var @event in eventsToSave)
    //         {
    //             if (salesContext.Database.ProviderName?.Contains("InMemory") != true)
    //             {
    //                 await using var transaction = await salesContext.Database.BeginTransactionAsync(cancellationToken);
    //                 salesContext.EventStore.Add(@event);
    //                 await salesContext.SaveChangesAsync(cancellationToken);
    //                 await transaction.CommitAsync(cancellationToken);
    //             }
    //             else
    //             {
    //                 // Skip transaction for InMemory provider
    //                 salesContext.EventStore.Add(@event);
    //                 await salesContext.SaveChangesAsync(cancellationToken);
    //             }
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         UtilitiesService.LogError(ex, _logger);
    //         throw;
    //     }
    // }
    
    #region IRepository Members
    public Task<TAggregate?> GetByIdAsync<TAggregate>(IDomainId id, CancellationToken cancellationToken = new())
        where TAggregate : class, IAggregate
    {
        return GetByIdAsync<TAggregate>(id, int.MaxValue, cancellationToken);
    }

    public Task<TAggregate?> GetByIdAsync<TAggregate>(IDomainId id, long version,
        CancellationToken cancellationToken = new()) where TAggregate : class, IAggregate
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders,
        CancellationToken cancellationToken = new())
    {
        var commitHeaders = new Dictionary<string, object>
        {
            { SqlPersistenceHelper.CommitIdHeader, commitId },
            { SqlPersistenceHelper.CommitDateHeader, DateTime.UtcNow},
            { SqlPersistenceHelper.AggregateClrTypeHeader, aggregate.GetType().AssemblyQualifiedName! }
        };
        updateHeaders(commitHeaders);

        var newEvents = aggregate.GetUncommittedEvents().Cast<object>().ToList();
        var eventsToSave = newEvents.Select(e => ToEventData(commitId, aggregate, e, commitHeaders)).ToList();

        try
        {
            foreach (var @event in eventsToSave)
            {
                if (warehouseContext.Database.ProviderName?.Contains("InMemory") != true)
                {
                    await using var transaction = await warehouseContext.Database.BeginTransactionAsync(cancellationToken);
                    await warehouseContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                else
                {
                    // Skip transaction for InMemory provider
                    await warehouseContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
        catch(Exception ex)
        {
            UtilitiesService.LogError(ex, _logger);
        }
        
        aggregate.ClearUncommittedEvents();
    }

    public async Task SaveAsync(IAggregate aggregate, Guid commitId, CancellationToken cancellationToken = new())
    {
        await SaveAsync(aggregate, commitId, headers => { }, cancellationToken);
    }
    
    private static TAggregate ConstructAggregate<TAggregate>()
    {
        return (TAggregate)Activator.CreateInstance(typeof(TAggregate), true)!;
    }
    
    private static EventRecord ToEventData(Guid eventId, IAggregate aggregate, object @event, IDictionary<string, object> headers)
    {
        var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event, SerializerSettings));
        var eventHeaders = new Dictionary<string, object>(headers) { { SqlPersistenceHelper.EventClrTypeHeader, @event.GetType().AssemblyQualifiedName! } };
        var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, SerializerSettings));
        var typeName = @event.GetType().Name;

        return EventRecord.Create(eventId, aggregate.Id.Value, aggregate.GetType().Name, aggregate.GetType().FullName!,
            typeName, data, metadata, aggregate.Version);
    }
    #endregion
    
    #region IDisposable Support
    private bool _disposedValue; // To detect redundant calls

    private void Dispose(bool disposing)
    {
        if (_disposedValue)
            return;
        
        if (disposing)
        {
            // TODO: dispose managed state (managed objects).
        }
        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.
        _disposedValue = true;
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~SalesOrderRepository() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        Dispose(true);
        // TODO: uncomment the following line if the finalizer is overridden above.
        // GC.SuppressFinalize(this);
    }
    #endregion
}

internal class PrivateContractResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Select(p => base.CreateProperty(p, memberSerialization))
            .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(f => base.CreateProperty(f, memberSerialization)))
            .ToList();
        props.ForEach(p => { p.Writable = true; p.Readable = true; });
        return props;
    }
}