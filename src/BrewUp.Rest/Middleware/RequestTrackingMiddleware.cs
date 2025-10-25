using System.Diagnostics;

namespace BrewUp.Rest.Middleware;

public class RequestTrackingMiddleware(RequestDelegate next, 
    ActivitySource activitySource)
{
    public async Task InvokeAsync(HttpContext context)
    {
        using var activity = activitySource.StartActivity($"{context.Request.Method} {context.Request.Path}");
        
        try
        {
            activity?.SetTag("http.method", context.Request.Method);
            activity?.SetTag("http.url", context.Request.Path);
            
            // Call the next delegate/middleware in the pipeline
            await next(context);
            
            activity?.SetTag("http.status_code", context.Response.StatusCode);
        }
        catch (Exception ex)
        {
            activity?.SetTag("error", true);
            activity?.SetTag("error.message", ex.Message);
            activity?.SetTag("error.stack_trace", ex.StackTrace);
            throw;
        }
    }
}