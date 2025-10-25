using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OpenApi;

namespace BrewUp.Sales.Facade.Endpoints;

public static class SalesEndpoints
{
    public static WebApplication MapSalesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/v1/sales")
            .WithTags("Sales")
            .WithOpenApi();

        group.MapGet("/", () => Results.Ok("Sales module is running"))
            .WithName("GetSalesStatus")
            .WithSummary("Get Sales module status")
            .WithDescription("Returns the status of the Sales module");

        return app;
    }
}