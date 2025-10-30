namespace BrewUp.Sales.Facade.FSharp

open System.Threading.Tasks
open BrewUp.Shared.FSharp.ExternalContracts
open BrewUp.Shared.FSharp.ReadModel

type ISalesFacade =
    abstract CreateSalesOrderAsync: CreateSalesOrderJson -> Task<string>
    abstract GetSalesOrdersAsync: page: int * pageSize: int -> Task<PagedResult<SalesOrderJson>>