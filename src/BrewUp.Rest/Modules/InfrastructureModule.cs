using BrewUp.InMemoryBroker;
using BrewUp.Shared.Validation;

namespace BrewUp.Rest.Modules;

public class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ValidationHandler>();
        builder.Services.AddInMemoryBroker();
        
        return builder.Services;
    }

    public WebApplication Configure(WebApplication app) => app;
}