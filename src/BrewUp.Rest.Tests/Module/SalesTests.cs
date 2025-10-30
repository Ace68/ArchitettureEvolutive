using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Text.Json;
using BrewUp.Shared.ExternalContracts;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Rest.Tests.Module;

[ExcludeFromCodeCoverage]
[Collection( "Integration Fixture" )]
public class SalesTests (AppHttpClientFixture integrationFixture)
{
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };
    
    [Fact]
    public async Task Can_Create_SalesOrder()
    {
        CreateSalesOrderJson body = new()
        {
            OrderNumber = "SO-001",
            OrderDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid().ToString(),
            CustomerName = "Il Grottino del Muflone",
            DeliveryDate = DateTime.UtcNow.AddDays(7),
            Rows = new List<SalesOrderRowJson>
            {
                new()
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = "BrewUp IPA",
                    Quantity = new ProductQuantity(10, "Bottles"),
                    Price = new ProductPrice(5, "EUR")
                }
            }
        };

        var stringJson = JsonSerializer.Serialize(body);
        var httpContent = new StringContent(stringJson, Encoding.UTF8, "application/json");
        var postResult = await integrationFixture.Client.PostAsync("/v1/sales", httpContent);
        
        Assert.Equal(HttpStatusCode.Created, postResult.StatusCode);
    }
    
    [Fact]
    public async Task Can_Get_SalesOrders()
    {
        var getResult = await integrationFixture.Client.GetAsync("/v1/sales");
        var content = await getResult.Content.ReadAsStringAsync();
        var pagedResult = JsonSerializer.Deserialize<PagedResult<SalesOrderJson>>(content, _options);

        Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);
        Assert.NotNull(pagedResult);
        Assert.NotNull(pagedResult.Results);
    }
}