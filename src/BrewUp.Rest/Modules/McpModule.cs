namespace BrewUp.Rest.Modules;

public class McpModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 0;

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
      builder.Services.AddMcpServer()
        .WithHttpTransport()
        .WithToolsFromAssembly();

        return builder.Services;
    }

  public WebApplication Configure(WebApplication app) => app;
}
