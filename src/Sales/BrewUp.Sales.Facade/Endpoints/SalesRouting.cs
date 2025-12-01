using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BrewUp.Sales.Facade.Endpoints;

public static class SalesRouting
{
    public static void DefineSalesRoutes(this WebApplication webApplication, SalesCompositionRoot compositionRoot)
    {
        var salesGroup = webApplication.MapGroup("/v1/sales")
            .WithTags("Sales");

        // salesGroup.MapPost("/", SalesOrderHandler.HandlePostCreateSalesOrder)
        //     .Produces(StatusCodes.Status201Created)
        //     .Produces(StatusCodes.Status500InternalServerError)
        //     .WithSummary("Create a new sales order")
        //     .WithDescription(
        //         "Creates a new sales order. This endpoint is used to add a new sales order.")
        //     .WithName("CreateSalesOrder");

        salesGroup.MapGet("/", SalesOrderHandler.HandleGetSalesOrders)
            .Produces<PagedResult<SalesOrderJson>>()
            .Produces(StatusCodes.Status500InternalServerError)
            .WithSummary("Get a list of sales orders")
            .WithDescription(
                "Get a list of sales orders.")
            .WithName("GetSalesOrder");
    }
}