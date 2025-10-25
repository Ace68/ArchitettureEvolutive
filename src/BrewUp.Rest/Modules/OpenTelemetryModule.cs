using System.Diagnostics;
using Azure.Monitor.OpenTelemetry.Exporter;
using BrewUp.Rest.Middleware;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace BrewUp.Rest.Modules;

public class OpenTelemetryModule : IModule
{
    public bool IsEnabled => false;
    public int Order => 0;
    
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        var serviceName = "BrewUpApi";
        var serviceVersion = typeof(OpenTelemetryModule).Assembly.GetName().Version?.ToString() ?? "1.0.0";

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(serviceName: serviceName, serviceVersion: serviceVersion)
                .AddEnvironmentVariableDetector())
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation(options =>
                {
                    options.RecordException = true;
                    options.EnrichWithHttpRequest = (activity, request) =>
                    {
                        activity.SetTag("http.request.headers", string.Join(",", request.Headers.Keys));
                    };
                    options.EnrichWithHttpResponse = (activity, response) =>
                    {
                        activity.SetTag("http.response.status_code", response.StatusCode);
                    };
                })
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation(options =>
                {
                    options.SetDbStatementForText = true;
                    options.RecordException = true;
                })
                .AddSource(serviceName)
                .AddAzureMonitorTraceExporter(o =>
                {
                    o.ConnectionString = builder.Configuration["ConnectionStrings:applicationInsights"];
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddAzureMonitorMetricExporter(options =>
                {
                    options.ConnectionString = builder.Configuration["ConnectionStrings:applicationInsights"];
                }));

        // Create and register a custom ActivitySource for manual instrumentation
        var activitySource = new ActivitySource(serviceName);
        builder.Services.AddSingleton(activitySource);

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.UseMiddleware<RequestTrackingMiddleware>();
        
        return app;
    }
}