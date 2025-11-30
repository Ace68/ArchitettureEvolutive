using BrewUp.Sales.Facade;
using BrewUp.Sales.Facade.Endpoints;

namespace BrewUp.Rest.Modules;

public class SalesModule : IModule
{
    public bool IsEnabled => false;
    public int Order => 3;
    
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddSalesFacade(builder.Configuration);
        
        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.MapSalesEndpoints();
        
        return app;
    }
}