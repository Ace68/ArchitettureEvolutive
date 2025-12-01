using BrewUp.Purchase.Facade;
using BrewUp.Purchase.Facade.Endpoints;

namespace BrewUp.Rest.Modules;

public class PurchaseModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddPurchaseFacade();
        
        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.MapPurchaseEndpoints();
        
        return app;
    }
}