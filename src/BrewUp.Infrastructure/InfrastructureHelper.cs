using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Infrastructure;

public static class InfrastructureHelper
{
    public static IServiceCollection AddBrewUpInfrastructure(this IServiceCollection services)
    {
        // Register shared infrastructure services here when needed
        
        return services;
    }
}

public class EventStoreSettings
{
    // Configuration settings for EventStore will be added later
}