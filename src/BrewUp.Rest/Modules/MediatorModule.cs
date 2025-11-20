using BrewUp.Mediator.Facade;
using BrewUp.Mediator.Facade.Endpoints;

namespace BrewUp.Rest.Modules;

public class MediatorModule : IModule
{
  public bool IsEnabled => true;

  public int Order => 0;

  public IServiceCollection Register(WebApplicationBuilder builder)
  {
    builder.Services.AddMediatorFacade();

    return builder.Services;
  }

  public WebApplication Configure(WebApplication app)
  {
    app.MapMediatorEndpoints();

    return app;
  }
}
