using BrewUp.Rest.Modules;
using BrewUp.Sales.Domain;
using BrewUp.Sales.Facade;
using BrewUp.Sales.Facade.Acl;
using BrewUp.Sales.Facade.Validators;
using BrewUp.Sales.Infrastructure;
using BrewUp.Sales.ReadModel;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using BrewUp.Shared.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Muflone;

var builder = WebApplication.CreateBuilder(args);

#region Sales
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateSalesOrderValidator>();

builder.Services.AddScoped<ISalesFacade, SalesFacade>();

builder.Services.AddSalesDomain();
builder.Services.AddSalesReadModel();
builder.Services.AddSalesInfrastructure(builder.Configuration);

builder.Services.AddIntegrationEventHandler<SalesOrderProductsPreparedEventHandler>();
#endregion

#region With Modules
WarehouseModule.Register(builder);
PurchaseModule.Register(builder);
OpenApiModule.Register(builder);
InfrastructureModule.Register(builder);
#endregion

var app = builder.Build();

#region Sales
var group = app.MapGroup("/v1/sales")
    .WithTags("Sales");

group.MapPost("/", HandlePostCreateSalesOrder)
    .Produces(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status500InternalServerError)
    .WithSummary("Create a new sales order")
    .WithDescription(
        "Creates a new sales order. This endpoint is used to add a new sales order.")
    .WithName("CreateSalesOrder");

group.MapGet("/", HandleGetSalesOrder)
    .Produces<PagedResult<SalesOrderJson>>()
    .Produces(StatusCodes.Status500InternalServerError)
    .WithSummary("Get a list of sales orders")
    .WithDescription(
        "Get a list of sales orders.")
    .WithName("GetSalesOrder");
#endregion

#region WithModules
WarehouseModule.Configure(app);
PurchaseModule.Configure(app);
InfrastructureModule.Configure(app);
OpenApiModule.Configure(app);
#endregion

await app.RunAsync();

#region private methods
async Task<IResult> HandlePostCreateSalesOrder(
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

async Task<IResult> HandleGetSalesOrder(
    ISalesOrderService salesOrderService,
    int page = 1,
    int pageSize = 10,
    CancellationToken cancellationToken = default)
{
    cancellationToken.ThrowIfCancellationRequested();

    PagedResult<SalesOrderJson> result =
        await salesOrderService.GetSalesOrdersAsync(page, pageSize, cancellationToken);

    return Results.Ok(result);
}
#endregion
