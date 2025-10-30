namespace BrewUp.Warehouse.Facade.FSharp

open System.Threading.Tasks

type IWarehouseFacade =
    abstract member GetHealthAsync: unit -> Task<bool>