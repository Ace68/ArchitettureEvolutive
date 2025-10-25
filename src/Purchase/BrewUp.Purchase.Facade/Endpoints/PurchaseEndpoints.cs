using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OpenApi;

namespace BrewUp.Purchase.Facade.Endpoints;

public static class PurchaseEndpoints
{
    public static WebApplication MapPurchaseEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/v1/purchase")
            .WithTags("Purchase")
            .WithOpenApi();

        group.MapGet("/", () => Results.Ok("Purchase module is running"))
            .WithName("GetPurchaseStatus")
            .WithSummary("Get Purchase module status")
            .WithDescription("Returns the status of the Purchase module");

        return app;
    }
}