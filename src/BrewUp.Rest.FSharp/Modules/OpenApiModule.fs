namespace BrewUp.Rest.FSharp.Modules

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.OpenApi.Models

type OpenApiModule() =
    interface IModule with
        member _.IsEnabled = true
        member _.Order = 0
        
        member _.Register(builder: WebApplicationBuilder) =
            builder.Services.AddEndpointsApiExplorer() |> ignore
            builder.Services.AddSwaggerGen(fun c ->
                c.SwaggerDoc("v1", OpenApiInfo(Title = "BrewUp API", Version = "v1"))
            ) |> ignore
            builder.Services
            
        member _.Configure(app: WebApplication) =
            if app.Environment.EnvironmentName = "Development" then
                app.UseSwagger() |> ignore
                app.UseSwaggerUI() |> ignore
            app