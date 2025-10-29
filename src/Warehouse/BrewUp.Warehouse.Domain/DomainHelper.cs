using BrewUp.Warehouse.Domain.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using Muflone;

namespace BrewUp.Warehouse.Domain;

public static class DomainHelper
{
    public static IServiceCollection AddWarehouseDomain(this IServiceCollection services)
    {
        services.AddCommandHandler<PrepareSalesOrderCommandHandler>();
        
        return services;
    }
}