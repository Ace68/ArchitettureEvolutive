using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Messages.Commands;
using BrewUp.Sales.SharedKernel.Messages.Events;
using BrewUp.Shared.ExternalContracts;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BrewUp.Sales.Tests.Entities;

public sealed class CreateSalesOrderSuccessfully : SalesCommandSpecification<CreateSalesOrder>
{
    private readonly SalesOrderId _salesOrderId = SalesOrderId.New();
    private readonly SalesOrderNumber _salesOrderNumber = new("SO-001");
    private readonly SalesOrderDate _salesOrderDate = new(DateTime.UtcNow);
    private readonly CustomerId _customerId = CustomerId.New();
    private readonly CustomerName _customerName = new("Il Grottino del Muflone");
    private readonly SalesOrderDeliveryDate _salesOrderDeliveryDate = new(DateTime.UtcNow.AddDays(7));
    private readonly IEnumerable<SalesOrderRowJson> _rows = [];
    
    private readonly Guid _correlationId = Guid.NewGuid();

    public CreateSalesOrderSuccessfully()
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

    protected override CreateSalesOrder When() => new (_salesOrderId,
        _salesOrderNumber,
        _salesOrderDate,
        _customerId,
        _customerName,
        _salesOrderDeliveryDate,
        _rows,
        _correlationId);

    protected override ICommandHandlerAsync<CreateSalesOrder> OnHandler()
    {
        return new CreateSalesOrderCommandHandler(Repository, new NullLoggerFactory());
    }
    
    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new SalesOrderCreated(_salesOrderId, _salesOrderNumber, _salesOrderDate, _customerId,
            _customerName, _salesOrderDeliveryDate, _rows.ToArray(), _correlationId);
    }
}