namespace BrewUp.Shared.FSharp.ReadModel

type PagedResult<'T> = {
    Results: 'T array
    PageSize: int
    Page: int
    TotalRecords: int
}