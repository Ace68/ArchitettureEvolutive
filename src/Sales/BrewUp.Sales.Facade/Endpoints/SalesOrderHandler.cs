using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BrewUp.Sales.Facade.Endpoints;

public delegate Task<Results<Ok<PagedResult<SalesOrderJson>>, NotFound>> HandleGetSalesOrders();

internal static class SalesOrderHandler
{
    // internal static async Task<IResult> HandlePostCreateSalesOrder(
    //     ISalesFacade salesFacade,
    //     IValidator<CreateSalesOrderJson> validator,
    //     ValidationHandler validationHandler,
    //     CreateSalesOrderJson body,
    //     CancellationToken cancellationToken)
    // {
    //     cancellationToken.ThrowIfCancellationRequested();
    //
    //     await validationHandler.ValidateAsync(validator, body);
    //     if (!validationHandler.IsValid)
    //         return Results.BadRequest(validationHandler.Errors);
    //
    //     try
    //     {
    //         string salesOrderId = await salesFacade.CreateSalesOrderAsync(body, cancellationToken);
    //         return Results.Created($"/v1/sales/{salesOrderId}", salesOrderId);
    //     }
    //     catch
    //     {
    //         return Results.BadRequest();
    //     }
    // }

    // internal static async Task<IResult> HandleGetSalesOrder(
    //     ISalesFacade salesFacade,
    //     int page = 1,
    //     int pageSize = 10,
    //     CancellationToken cancellationToken = default)
    // {
    //     cancellationToken.ThrowIfCancellationRequested();
    //
    //     PagedResult<SalesOrderJson> result =
    //         await salesFacade.GetSalesOrdersAsync(page, pageSize, cancellationToken);
    //
    //     return Results.Ok(result);
    // }
    
    internal static HandleGetSalesOrders HandleGetSalesOrders(GetSalesOrders getGetSalesOrders) => async () =>
    {
        var orders = await getGetSalesOrders(0, 30);
        return TypedResults.Ok(orders);
    };
}