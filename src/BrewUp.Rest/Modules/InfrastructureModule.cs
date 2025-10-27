using BrewUp.InMemoryBroker;

namespace BrewUp.Rest.Modules;

public class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddInMemoryBroker();
        
        return builder.Services;
    }

    public WebApplication Configure(WebApplication app) => app;
}