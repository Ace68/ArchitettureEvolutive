using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading;

namespace BrewUp.Sales.Facade.McpTools;

[McpServerToolType]
public class SalesMcpTools(ISalesFacade salesFacade)
{
  [McpServerTool]
  [Description("Gets the sales order splitted by page")]
  public async Task<PagedResult<SalesOrderJson>> GetSalesOrders(int page, int pageSize)
  {
    return await salesFacade.GetSalesOrdersAsync(page, pageSize, CancellationToken.None);
  }
}
