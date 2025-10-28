using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Messages.Events;
using BrewUp.Shared.Domain;
using BrewUp.Shared.ExternalContracts;

namespace BrewUp.Sales.Entities.Entities;

public class SalesOrder : BrewUpAggregateRoot
{
    public string SalesOrderNumber { get; private set; } = string.Empty;
    public DateTime SalesOrderDate { get; private set; }
    
    public string CustomerId { get; private set; } = string.Empty;
    public string CustomerName { get; private set; } = string.Empty;
    
    public DateTime SalesOrderDeliveryDate { get; private set; }
    
    public virtual ICollection<SalesOrderRow> SalesOrderRows { get; private set; } = [];
    
    public string Status { get; private set; } = string.Empty;
    
    protected SalesOrder()
    {}

    public static SalesOrder Create(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber,
        SalesOrderDate salesOrderDate, CustomerId customerId, CustomerName customerName,
        SalesOrderDeliveryDate salesOrderDeliveryDate, IEnumerable<SalesOrderRowJson> rows)
    {
        return new SalesOrder(salesOrderId, salesOrderNumber, salesOrderDate, customerId, customerName,
            salesOrderDeliveryDate, rows);
    }

    private SalesOrder(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, SalesOrderDate salesOrderDate,
        CustomerId customerId, CustomerName customerName, SalesOrderDeliveryDate salesOrderDeliveryDate,
        IEnumerable<SalesOrderRowJson> rows)
    {
        var rowsArray = rows.ToArray();
        
        Id = salesOrderId.Value;
        SalesOrderNumber = salesOrderNumber.Value;
        SalesOrderDate = salesOrderDate.Value;
        CustomerId = customerId.Value;
        CustomerName = customerName.Value;
        SalesOrderDeliveryDate = salesOrderDeliveryDate.Value;
        SalesOrderRows = rowsArray.Select(row => SalesOrderRow.Create(new SalesOrderId(Id),
            new ProductId(row.ProductId), new ProductName(row.ProductName),
            row.Quantity, row.Price)).ToList();
        
        Status = "Created";
    
        RaiseEvent(new SalesOrderCreated(salesOrderId, salesOrderNumber, salesOrderDate, customerId, customerName, salesOrderDeliveryDate, rowsArray));
    }
}