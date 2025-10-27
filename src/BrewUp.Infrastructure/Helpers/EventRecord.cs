namespace BrewUp.Infrastructure.Helpers;

public class EventRecord
{
    public string MessageId { get; private set; } = string.Empty;
    public string AggregateId { get; private set; } = string.Empty;
    public string AggregateName { get; private set; } = string.Empty;
    public string AggregateType { get; private set; } = string.Empty;
    public string EventType { get; private set; } = string.Empty;
    public byte[] Data { get; private set; } = [];
    public byte[] Metadata { get; private set; } = [];
    public int Version { get; private set; }
    public long CommitPosition { get; private set; }
    
    protected EventRecord()
    {}

    public static EventRecord Create(Guid messageId, string aggregateId, string aggregateName, string aggregateType,
        string eventType, byte[] data, byte[] metadata, int version)
    {
        return new EventRecord(messageId, aggregateId, aggregateName, aggregateType, eventType, data, metadata, version);
    }

    private EventRecord(Guid messageId, string aggregateId, string aggregateName, string aggregateType,
        string eventType, byte[] data, byte[] metadata, int version)
    {
        MessageId = messageId.ToString();
        AggregateId = aggregateId;
        AggregateName = aggregateName;
        AggregateType = aggregateType;
        Data = data;
        EventType = eventType;
        Metadata = metadata;
        Version = version;
    }
}