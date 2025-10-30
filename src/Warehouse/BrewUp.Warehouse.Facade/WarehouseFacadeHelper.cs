using BrewUp.Warehouse.Domain;
using BrewUp.Warehouse.Facade.Acl;
using BrewUp.Warehouse.Infrastructure;
using BrewUp.Warehouse.ReadModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muflone;

namespace BrewUp.Warehouse.Facade;

public static class WarehouseFacadeHelper
{
    public static IServiceCollection AddWarehouseFacade(this IServiceCollection services,
        IConfigurationManager configurationManager)
    {
        // Register any services related to the Warehouse facade here
        services.AddScoped<IWarehouseFacade, WarehouseFacade>();

        services.AddWarehouseDomain();
        services.AddWarehouseInfrastructure(configurationManager);
        services.AddWarehouseReadModel();

        services.AddIntegrationEventHandler<SalesOrderReadyForProcessingEventHandler>();

        return services;
    }
}