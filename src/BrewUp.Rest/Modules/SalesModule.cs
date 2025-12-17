using BrewUp.Sales.Facade;
using BrewUp.Sales.Facade.Endpoints;
using Serilog.Core;

namespace BrewUp.Rest.Modules;

public class SalesModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    private Logger _logger = null!;

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        _logger = Logging.Build(builder.Configuration);
        builder.Services.AddSalesFacade(builder.Configuration);
        
        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        // var salesCompositionRoot = SalesCompositionRoot.Build(_logger);
        // app.DefineSalesRoutes(salesCompositionRoot);
        
        return app;
    }
}