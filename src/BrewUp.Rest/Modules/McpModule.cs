using BrewUp.Sales.Facade;
using BrewUp.Warehouse.Facade;
using System;

namespace BrewUp.Rest.Modules;

public class McpModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 0;

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
      builder.Services.AddMcpServer(options =>
      {
        options.ServerInfo = new ()
        {
          Name = "BrewUp Management Control Protocol",
          Version = "1.0.0",
        };

        options.ServerInstructions = "Welcome to BrewUp MCP Server. You are an assistant to use to interact with the BrewUp system.";
      })
        .WithHttpTransport()
        .WithToolsFromAssembly(typeof(SalesFacadeHelper).Assembly);

        return builder.Services;
    }

  public WebApplication Configure(WebApplication app) => app;
}
