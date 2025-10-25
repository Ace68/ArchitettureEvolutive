using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace BrewUp.Rest.Modules;

public class OpenApiModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Servers = [new OpenApiServer {Url = "/"}];
                document.Info = new OpenApiInfo
                {
                    Title = "BrewUp API",
                    Version = "v1.0",
                    Description = "BrewUp API for managing the preparation of Brescia-style skewer",
                    Contact = new OpenApiContact
                    {
                        Name = "BrewUp"
                    }
                };

                return Task.CompletedTask;
            });
        });

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.WithTitle("BrewUp API")
                .WithTheme(ScalarTheme.None);
        });

        return app;
    }
}