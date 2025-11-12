using BrewUp.InMemoryBroker;
using BrewUp.Shared.Validation;

namespace BrewUp.Rest.Modules;

public static class InfrastructureModule
{
  public static IServiceCollection Register(WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<ValidationHandler>();
    builder.Services.AddInMemoryBroker();

    return builder.Services;
  }

  public static WebApplication Configure(WebApplication app) => app;
}