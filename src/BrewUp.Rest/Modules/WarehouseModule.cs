using BrewUp.Warehouse.Facade;
using BrewUp.Warehouse.Facade.Endpoints;

namespace BrewUp.Rest.Modules;

public class WarehouseModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddWarehouseFacade(builder.Configuration);
        
        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.MapWarehouseEndpoints();
        
        return app;
    }
}