namespace BrewUp.Infrastructure.FSharp.Helpers

type ResolvedEvent = {
    MessageId: string
    AggregateId: string
    AggregateName: string
    AggregateType: string
    EventType: string
    Data: string
    Metadata: string
    Version: int
    CommitPosition: int64
}