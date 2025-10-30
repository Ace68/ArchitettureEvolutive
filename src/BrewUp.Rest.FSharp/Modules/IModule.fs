namespace BrewUp.Rest.FSharp.Modules

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection

type IModule =
    abstract IsEnabled: bool
    abstract Order: int
    abstract Register: WebApplicationBuilder -> IServiceCollection
    abstract Configure: WebApplication -> WebApplication