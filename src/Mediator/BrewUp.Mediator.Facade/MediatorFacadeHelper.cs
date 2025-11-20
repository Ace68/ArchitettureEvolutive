using BrewUp.Mediator.Facade.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Mediator.Facade;

public static class MediatorFacadeHelper
{
    public static IServiceCollection AddMediatorFacade(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateSalesOrderValidator>();
        
        services.AddScoped<IMediatorFacade, MediatorFacade>();

        return services;
    }
}