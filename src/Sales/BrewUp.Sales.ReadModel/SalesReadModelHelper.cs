using BrewUp.Sales.Entities.Entities;
using BrewUp.Sales.ReadModel.Queries;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.ReadModel;

public static class SalesReadModelHelper
{
    public static IServiceCollection AddSalesReadModel(this IServiceCollection services)
    {
        services.AddScoped<IQueries<SalesOrder>, SalesOrderQuery>();
        services.AddScoped<ISalesOrderService, SalesOrderService>();

        return services;
    }
}