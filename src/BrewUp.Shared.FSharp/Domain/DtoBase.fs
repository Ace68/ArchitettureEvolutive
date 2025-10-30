namespace BrewUp.Shared.FSharp.Domain

[<AbstractClass>]
type DtoBase() =
    member val Id: string = "" with get, set