using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BrewUp.Infrastructure.Helpers;

public static class SqlPersistenceHelper
{
    public const string EventClrTypeHeader = "EventClrTypeName";
    public const string AggregateClrTypeHeader = "AggregateClrTypeName";
    public const string CommitIdHeader = "CommitId";
    public const string CommitDateHeader = "CommitDate";
    
    public static object DeserializeEvent(EventRecord resolvedEvent)
    {
        var eventClrTypeName = JObject.Parse(Encoding.UTF8.GetString(resolvedEvent.Metadata.ToArray())).Property(EventClrTypeHeader)!.Value;
        return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(resolvedEvent.Data.ToArray()), Type.GetType(((string)eventClrTypeName)!)!)!;        
    }

    public static ResolvedEvent ConvertToResolvedEvent(this EventRecord eventRecord)
    {
        return new ResolvedEvent
        {
            MessageId = eventRecord.MessageId,
            AggregateId = eventRecord.AggregateId,
            AggregateName = eventRecord.AggregateName,
            AggregateType = eventRecord.AggregateType,
            EventType = eventRecord.EventType,
            Data = Encoding.UTF8.GetString(eventRecord.Data.ToArray()),
            Metadata = Encoding.UTF8.GetString(eventRecord.Metadata.ToArray()),
            Version = eventRecord.Version,
            CommitPosition = eventRecord.CommitPosition
        };
    }
}