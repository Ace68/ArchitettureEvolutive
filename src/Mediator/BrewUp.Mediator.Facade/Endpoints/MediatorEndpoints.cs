using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BrewUp.Mediator.Facade.Endpoints;

public static class MediatorEndpoints
{
  public static WebApplication MapMediatorEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/v1/sales")
        .WithTags("Sales");

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
      IMediatorFacade mediatorFacade,
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
      string salesOrderId = await mediatorFacade.CreateSalesOrderAsync(body, cancellationToken);
      return Results.Created($"/v1/sales/{salesOrderId}", salesOrderId);
    }
    catch
    {
      return Results.BadRequest();
    }
  }
}