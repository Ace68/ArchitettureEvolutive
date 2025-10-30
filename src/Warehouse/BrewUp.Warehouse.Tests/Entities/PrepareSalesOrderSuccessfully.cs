using BrewUp.Shared.ExternalContracts;
using BrewUp.Warehouse.Domain.CommandHandlers;
using BrewUp.Warehouse.SharedKernel.CustomTypes;
using BrewUp.Warehouse.SharedKernel.Messages.Commands;
using BrewUp.Warehouse.SharedKernel.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.Tests.Entities;

public sealed class PrepareSalesOrderSuccessfully : WarehouseCommandSpecification<PrepareSalesOrder>
{
    private readonly SalesOrderId _salesOrderId = SalesOrderId.New();
    private readonly SalesOrderNumber _salesOrderNumber = new("SO-001");
    private readonly SalesOrderDeliveryDate _salesOrderDeliveryDate = new(DateTime.UtcNow.AddDays(7));
    private readonly IEnumerable<SalesOrderRowJson> _rows = [];
    
    private readonly Guid _correlationId = Guid.NewGuid();
    
    public PrepareSalesOrderSuccessfully()
    {
        _rows = _rows.Concat(new List<SalesOrderRowJson>
        {
            new ()
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductName = "Muflone IPA 33cl",
                Quantity = new ProductQuantity(24, "Bottles"),
                Price = new ProductPrice(5, "EUR")
            }
        });
    }

    protected override IEnumerable<DomainEvent> Given() => [];

    protected override PrepareSalesOrder When() => new PrepareSalesOrder(_salesOrderId, _salesOrderNumber,
        _salesOrderDeliveryDate, _rows, _correlationId);

    protected override ICommandHandlerAsync<PrepareSalesOrder> OnHandler() =>
        new PrepareSalesOrderCommandHandler(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new AvailabilityUpdated(_salesOrderId, _salesOrderNumber, _rows, _correlationId);
    }
}