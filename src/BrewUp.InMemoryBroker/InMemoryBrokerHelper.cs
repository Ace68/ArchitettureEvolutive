using Microsoft.Extensions.DependencyInjection;
using Muflone;
using Muflone.Messages;
using Muflone.Persistence;

namespace BrewUp.InMemoryBroker;

public static class InMemoryBrokerHelper
{
    public static IServiceCollection AddInMemoryBroker(this IServiceCollection services)
    {
        services.AddSingleton<IMessageSubscriber, InMemorySubscriber>();
        services.AddKeyedSingleton<IServiceBus, InMemoryBus>("InMemory");
        services.AddKeyedSingleton<IEventBus, InMemoryBus>("InMemory");

        services.AddHostedService<MessageHandlersStarter>();
        
        return services;
    }
}