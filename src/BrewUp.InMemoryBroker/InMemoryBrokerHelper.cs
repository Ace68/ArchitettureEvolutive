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
        services.AddSingleton<IServiceBus, InMemoryBus>();
        services.AddSingleton<IEventBus, InMemoryBus>();

        services.AddHostedService<MessageHandlersStarter>();
        
        return services;
    }
}