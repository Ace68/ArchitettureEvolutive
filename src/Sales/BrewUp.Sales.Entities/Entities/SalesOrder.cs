using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Domain;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.Entities.Entities;

public class SalesOrder : BrewUpAggregateRoot
{
    public string SalesOrderNumber { get; private set; } = string.Empty;
    public DateTime SalesOrderDate { get; private set; }
    
    public string CustomerId { get; private set; } = string.Empty;
    public string CustomerName { get; private set; } = string.Empty;
    
    public DateTime SalesOrderDeliveryDate { get; private set; }
    
    public IEnumerable<SalesOrderRow> SalesOrderRows { get; private set; } = [];
    
    protected SalesOrder()
    {}

    internal static SalesOrder Create(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber,
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
        // _salesOrderNumber = salesOrderNumber;
        // _salesOrderDate = salesOrderDate;
        //
        // _customerId = customerId;
        // _customerName = customerName;
        //
        // _salesOrderDeliveryDate = salesOrderDeliveryDate;
    }
}