using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using BrewUp.Shared.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BrewUp.Sales.Facade.Endpoints;

public static class SalesEndpoints
{
  public static WebApplication MapSalesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/v1/sales")
        .WithTags("Sales");

    group.MapGet("/", HandleGetSalesOrder)
        .Produces<PagedResult<SalesOrderJson>>()
        .Produces(StatusCodes.Status500InternalServerError)
        .WithSummary("Get a list of sales orders")
        .WithDescription(
            "Get a list of sales orders.")
        .WithName("GetSalesOrder");

    return app;
  }

  private static async Task<IResult> HandleGetSalesOrder(
      ISalesFacade salesFacade,
      int page = 1,
      int pageSize = 10,
      CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();

    PagedResult<SalesOrderJson> result =
        await salesFacade.GetSalesOrdersAsync(page, pageSize, cancellationToken);

    return Results.Ok(result);
  }
}