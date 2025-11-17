using BrewUp.Sales.Entities.Entities;
using BrewUp.Sales.Infrastructure.Repository;
using BrewUp.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Infrastructure;

public static class InfrastructureHelper
{
    public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services,
        IConfigurationManager configurationManager)
    {
        services.AddDbContext<SalesContext>(options =>
            options.UseSqlServer(configurationManager["BrewUp:SqlServer:ConnectionString"]!));

    services.AddScoped<IBrewUpRepository<SalesOrder>, SalesOrderRepository>();
        
        return services;
    }
}