using BrewUp.Warehouse.ReadModel.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Muflone;

namespace BrewUp.Warehouse.ReadModel;

public static class WarehouseReadModelHelper
{
    public static IServiceCollection AddWarehouseReadModel(this IServiceCollection services)
    {
        services.AddDomainEventHandler<AvailabilityUpdatedEventHandler>();

        return services;
    }
}