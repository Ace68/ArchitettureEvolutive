using BrewUp.Warehouse.Domain;
using BrewUp.Warehouse.Facade;
using BrewUp.Warehouse.Facade.Acl;
using BrewUp.Warehouse.Infrastructure;
using BrewUp.Warehouse.ReadModel;
using Muflone;

namespace BrewUp.Rest.Modules;

public static class WarehouseModule
{
  public static IServiceCollection Register(WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<IWarehouseFacade, WarehouseFacade>();

    builder.Services.AddWarehouseDomain();
    builder.Services.AddWarehouseInfrastructure(builder.Configuration);
    builder.Services.AddWarehouseReadModel();

    builder.Services.AddIntegrationEventHandler<SalesOrderReadyForProcessingEventHandler>();

    return builder.Services;
  }

  public static WebApplication Configure(WebApplication app)
  {
    var group = app.MapGroup("/v1/warehouse")
      .WithTags("Warehouse");

    group.MapGet("/", () => Results.Ok("Warehouse module is running"))
      .WithName("GetWarehouseStatus")
      .WithSummary("Get Warehouse module status")
      .WithDescription("Returns the status of the Warehouse module");

    return app;
  }
}