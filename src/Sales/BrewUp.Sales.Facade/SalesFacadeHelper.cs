using BrewUp.Sales.Domain;
using BrewUp.Sales.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Facade;

public static class SalesFacadeHelper
{
    public static IServiceCollection AddSalesFacade(this IServiceCollection services)
    {
        // Register any services related to the Sales facade here
        services.AddScoped<ISalesFacade, SalesFacade>();

        services.AddSalesDomain();
        services.AddSalesInfrastructure();

        return services;
    }
}

internal class SalesFacade : ISalesFacade
{
    // Implementation will be added later
}