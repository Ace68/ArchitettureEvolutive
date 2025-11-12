using BrewUp.Purchase.Domain;
using BrewUp.Purchase.Facade;
using BrewUp.Purchase.Infrastructure;

namespace BrewUp.Rest.Modules;

public static class PurchaseModule
{
  public static IServiceCollection Register(WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<IPurchaseFacade, PurchaseFacade>();

    builder.Services.AddPurchaseDomain();
    builder.Services.AddPurchaseInfrastructure();

    return builder.Services;
  }

  public static WebApplication Configure(WebApplication app)
  {
    var group = app.MapGroup("/v1/purchase")
      .WithTags("Purchase");

    group.MapGet("/", () => Results.Ok("Purchase module is running"))
      .WithName("GetPurchaseStatus")
      .WithSummary("Get Purchase module status")
      .WithDescription("Returns the status of the Purchase module");

    return app;
  }
}