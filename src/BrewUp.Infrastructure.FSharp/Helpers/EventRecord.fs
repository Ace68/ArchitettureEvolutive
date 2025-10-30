namespace BrewUp.Infrastructure.FSharp.Helpers

open System

type EventRecord = {
    MessageId: string
    AggregateId: string
    AggregateName: string
    AggregateType: string
    EventType: string
    Data: byte array
    Metadata: byte array
    Version: int
    CommitPosition: int64
}

module EventRecord =
    let create (messageId: Guid) aggregateId aggregateName aggregateType eventType data metadata version =
        {
            MessageId = messageId.ToString()
            AggregateId = aggregateId
            AggregateName = aggregateName
            AggregateType = aggregateType
            EventType = eventType
            Data = data
            Metadata = metadata
            Version = version
            CommitPosition = 0L
        }