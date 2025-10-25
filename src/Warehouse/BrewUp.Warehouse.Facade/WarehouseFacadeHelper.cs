using BrewUp.Warehouse.Domain;
using BrewUp.Warehouse.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouse.Facade;

public static class WarehouseFacadeHelper
{
    public static IServiceCollection AddWarehouseFacade(this IServiceCollection services)
    {
        // Register any services related to the Warehouse facade here
        services.AddScoped<IWarehouseFacade, WarehouseFacade>();

        services.AddWarehouseDomain();
        services.AddWarehouseInfrastructure();

        return services;
    }
}

internal class WarehouseFacade : IWarehouseFacade
{
    // Implementation will be added later
}