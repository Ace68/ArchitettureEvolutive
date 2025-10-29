using BrewUp.Shared.Domain;
using BrewUp.Warehouse.Entities.Entities;
using BrewUp.Warehouse.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouse.Infrastructure;

public static class InfrastructureHelper
{
    public static IServiceCollection AddWarehouseInfrastructure(this IServiceCollection services,
        IConfigurationManager configurationManager)
    {
        services.AddDbContext<WarehouseContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString("sqlServer")!));
        
        services.AddScoped<IBrewUpRepository<Product>, ProductRepository>();
        
        return services;
    }
}