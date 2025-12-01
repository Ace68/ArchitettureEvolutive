using BrewUp.Sales.Entities.Entities;
using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Queries;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Muflone;

namespace BrewUp.Sales.ReadModel;

public static class SalesReadModelHelper
{
    public static IServiceCollection AddSalesReadModel(this IServiceCollection services)
    {
        services.AddScoped<IQueries<SalesOrder>, SalesOrderQuery>();

        services.AddDomainEventHandler<SalesOrderCreatedEventHandler>();

        return services;
    }
}