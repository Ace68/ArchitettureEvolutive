using System.Threading.Channels;
using Muflone.Messages.Events;

namespace BrewUp.InMemoryBroker;

public interface IInMemoryChannel
{
    Channel<object> Channel { get; }
}

public interface IInMemoryChannel<T> : IInMemoryChannel
    where T : class, IEvent
{
}