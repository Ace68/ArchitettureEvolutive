using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OpenApi;

namespace BrewUp.Warehouse.Facade.Endpoints;

public static class WarehouseEndpoints
{
    public static WebApplication MapWarehouseEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/v1/warehouse")
            .WithTags("Warehouse")
            .WithOpenApi();

        group.MapGet("/", () => Results.Ok("Warehouse module is running"))
            .WithName("GetWarehouseStatus")
            .WithSummary("Get Warehouse module status")
            .WithDescription("Returns the status of the Warehouse module");

        return app;
    }
}