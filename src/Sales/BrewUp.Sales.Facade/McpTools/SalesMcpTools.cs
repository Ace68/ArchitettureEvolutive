using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading;

namespace BrewUp.Sales.Facade.McpTools;

[McpServerToolType]
public class SalesMcpTools(ISalesFacade salesFacade)
{
  [McpServerTool(Name="get_sales_orders", Title = "Returns the list of sales orders")]
  [Description("Gets the sales order splitted by page using BrewUp API")]
  public async Task<PagedResult<SalesOrderJson>> GetSalesOrders([Description("Specify the number of page and its dimensione")]int page, int pageSize)
  {
    return await salesFacade.GetSalesOrdersAsync(page, pageSize, CancellationToken.None);
  }
}
