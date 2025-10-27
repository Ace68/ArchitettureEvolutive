namespace BrewUp.Infrastructure.Helpers;

public class ResolvedEvent
{
    public string MessageId { get; set; } = string.Empty;
    public string AggregateId { get; set; }  = string.Empty;
    public string AggregateName { get; set; } = string.Empty;
    public string AggregateType { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public string Metadata { get; set; } = string.Empty;
    public int Version { get; set; }
    public long CommitPosition { get; set; }
}