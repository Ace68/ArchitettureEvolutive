using BrewUp.Sales.Domain;
using BrewUp.Sales.Facade.Validators;
using BrewUp.Sales.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Facade;

public static class SalesFacadeHelper
{
    public static IServiceCollection AddSalesFacade(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateSalesOrderValidator>();
        
        services.AddScoped<ISalesFacade, SalesFacade>();

        services.AddSalesDomain();
        services.AddSalesInfrastructure();

        return services;
    }
}