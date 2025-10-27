using System.Threading.Channels;
using Muflone.Messages;
using Muflone.Messages.Events;

namespace BrewUp.InMemoryBroker;

public class InMemoryChannel(
    HandlerSubscription<IInMemoryChannel> handlerSubscription,
    Channel<object> channel,
    string routingKey) : IInMemoryChannel<IEvent>
{
    public HandlerSubscription<IInMemoryChannel> HandlerSubscription { get; } = handlerSubscription;
    public Channel<object> Channel { get; } = channel;
    public string RoutingKey { get; } = routingKey;
}