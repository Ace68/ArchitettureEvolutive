using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace BrewUp.Rest.Modules;

public static class OpenApiModule
{
  public static IServiceCollection Register(WebApplicationBuilder builder)
  {
    builder.Services.AddOpenApi(options =>
    {
      options.AddDocumentTransformer((document, _, _) =>
          {
            document.Servers = [new OpenApiServer { Url = "/" }];
            document.Info = new OpenApiInfo
            {
              Title = "BrewUp API",
              Version = "v1.0",
              Description = "BrewUp API for managing a Brewery",
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

  public static WebApplication Configure(WebApplication app)
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