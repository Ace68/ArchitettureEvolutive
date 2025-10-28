using BrewUp.Sales.Domain;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.Validation;
using FluentValidation;
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

        group.MapPost("/", HandlePostCreateSalesOrder)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithSummary("Create a new sales order")
            .WithDescription(
                "Creates a new sales order. This endpoint is used to add a new sales order.")
            .WithName("CreateSalesOrder");

        return app;
    }

    private static async Task<IResult> HandlePostCreateSalesOrder(
        ISalesDomainService salesDomainService,
        IValidator<CreateSalesOrderJson> validator,
        ValidationHandler validationHandler,
        CreateSalesOrderJson body,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        try
        {
            string salesOrderId = await salesDomainService.CreateSalesOrderAsync(body, cancellationToken);
            return Results.Created($"/v1/sales/{salesOrderId}", salesOrderId);
        }
        catch
        {
            return Results.BadRequest();
        }
    }
}