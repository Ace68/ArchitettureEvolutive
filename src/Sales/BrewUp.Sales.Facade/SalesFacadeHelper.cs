using BrewUp.Sales.Domain;
using BrewUp.Sales.Facade.Acl;
using BrewUp.Sales.Infrastructure;
using BrewUp.Sales.ReadModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muflone;

namespace BrewUp.Sales.Facade;

public static class SalesFacadeHelper
{
    public static IServiceCollection AddSalesFacade(this IServiceCollection services,
        IConfigurationManager configurationManager)
    {
        services.AddScoped<ISalesFacade, SalesFacade>();

        services.AddSalesDomain();
        services.AddSalesReadModel();
        services.AddSalesInfrastructure(configurationManager);

        services.AddIntegrationEventHandler<SalesOrderProductsPreparedEventHandler>();

        return services;
    }
}