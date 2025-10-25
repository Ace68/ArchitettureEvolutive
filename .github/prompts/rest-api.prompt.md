# Instructions for Implementing a New .NET API Using a Template

## Initial Feature
1. Use the glossary of terms to understand key concepts found in `glossary.prompt.md` and follow the DDD directives in `domain-driven-design.prompt.md`.  
2. Use the **.NET 9 Minimal API** model to implement the APIs.  
3. You **must** use the template available at [https://github.com/BrewUp/CrastuArrustutu](https://github.com/BrewUp/CrastuArrustutu). Do **not** use the default .NET 9 template.  
   - If you cannot access the template, try cloning the repository.  
   - If cloning the repository fails, stop.  
   - The template includes the example project `CrastuArrustutu.Rest`, which demonstrates how to implement a RESTful API using a custom Minimal API model.  
   - The `CrastuArrustutu.Rest` project already includes configurations for **OpenAPI**, **Swagger**, **OpenTelemetry**, and more — use them.  
   - The `CrastuArrustutu.Rest` project also includes configurations for documentation and metrics management — use them.  
   - The `CrastuArrustutu.Rest` project contains an `IModule` interface you must use in your modules, as well as other predefined classes and interfaces.  
   - The example project contains `OpenApiModule` and `OpenTelemetryModule` — use these to expose OpenAPI documentation and monitoring metrics.  
   - The `ModuleExtensions` file provides helper methods for registering modules.  
   - The provided `program.cs` file is ready to use in the final solution.  
   - The example files `CarnizzaroModule.cs` and `TannuraModule.cs` show how to invoke methods from their respective Facade projects. Follow the same pattern for the new projects `{BrewUp}.{Purchase}.Facade`, `{BrewUp}.{Warehouse}.Facade` and `{BrewUp}.{Sales}.Facade`.  
   - All example projects in the template use the `{CrastuArrustutu}` prefix, but you must use the `{BrewUp}` prefix for your own projects.  
   - Maintain the structure of `CrastuArrustutu.Rest`, including the `Modules` folder, and place all module files inside it.  
   - When creating a new project, remove the default `Class1.cs` file.  

4. In the Rest project of the new application, create the module files (`WarehouseModule`, `PurchaseModule` and `SalesModule`) that implement `IModule` to handle the `Purchase`, `Warehouse` and `Sales` Bounded Contexts.  
5. In the `{BrewUp}.{Purchase}.Facade`, `{BrewUp}.{Warehouse}.Facade` and `{BrewUp}.{Sales}.Facade` projects, create the classes that expose endpoints following the examples in `Cannizzaro/CrastuArrustutu.Cannizzaro.Facade` and `Tannura/CrastuArrustutu.Tannura.Facade`.  
   - Use `CarnizzaroFacadeHelper` and `TannuraFacadeHelper` as references for your helpers.  
   - Use `CarnizzaroEndpoints` and `TannuraEndpoints` as references for your endpoints.  
   - The Facade projects must expose the interfaces `IPurchaseFacade`, `ISalesFacade` and `IWarehouseFacade`.  

6. Split the solution into multiple projects organized under **solution folders** (logical structure only, not physical directories). The only physical folders should be those for the `Purchase` and `Warehouse` modules. Follow the organization shown in the CrastuArrustutu repository and keep the same folder naming conventions:

   - **Solution Folder `90 Presentation`**  
     - One single project for the REST API.  
     - The REST project must expose OpenAPI and Swagger documentation following the template (via an `OpenApiModule`).  
     - It must expose monitoring metrics compatible with OpenTelemetry through an `OpenTelemetryModule`.  
     - It should depend on the `Purchase`, `Warehouse` and `Sales` modules **only** through their Facade projects.  
     - When implementing `PurchaseModule`, `SalesModule` and `WarehouseModule`, ensure their endpoint registration methods implement the `IModule` interface from the template and follow the naming and structure conventions. They must call the corresponding Facade methods to register endpoints and dependencies.  

   - **Solution Folder `80 Infrastructure`**  
     - `BrewUp.Infrastructure`: data access and repository implementations.  

   - **Solution Folder `50 Modules`**  
     - Inside this folder, create one subfolder per module (`Purchase` and `Warehouse`).  

   - **Solution Folder `Purchase`**  
     - `BrewUp.Purchase.Facade`: exposes endpoints and the `IPurchaseFacade` interface.  
     - `BrewUp.Purchase.Domain`: contains entities and domain logic.  
     - `BrewUp.Purchase.SharedKernel`: shared classes, custom types, and interfaces within the Purchase module.  
     - `BrewUp.Purchase.ReadModel`: read model classes and queries.  
     - `BrewUp.Purchase.Infrastructure`: data access and repository implementations.  
     - `BrewUp.Purchase.Tests`: architectural tests for the Purchase module.  

   - **Solution Folder `Warehouse`**  
     - `BrewUp.Warehouse.Facade`: exposes endpoints and the `IWarehouseFacade` interface.  
     - `BrewUp.Warehouse.Domain`: contains entities and domain logic.  
     - `BrewUp.Warehouse.SharedKernel`: shared classes, custom types, and interfaces within the Warehouse module.  
     - `BrewUp.Warehouse.ReadModel`: read model classes and queries.  
     - `BrewUp.Warehouse.Infrastructure`: data access and repository implementations.  
     - `BrewUp.Warehouse.Tests`: architectural tests for the Warehouse module.  

   - **Solution Folder `Sales`**  
     - `BrewUp.Sales.Facade`: exposes endpoints and the `ISalesFacade` interface.  
     - `BrewUp.Sales.Domain`: contains entities and domain logic.  
     - `BrewUp.Sales.SharedKernel`: shared classes, custom types, and interfaces within the Sales module.  
     - `BrewUp.Sales.ReadModel`: read model classes and queries.  
     - `BrewUp.Sales.Infrastructure`: data access and repository implementations.  
     - `BrewUp.Sales.Tests`: architectural tests for the Sales module.  

   - **Solution Folder `30 Shared`**  
     - `BrewUp.Shared`: shared classes, custom types, and interfaces across the entire solution.  

7. Do **not** create Entities, Repositories, or Services specific to `Purchase`, `Sales` and `Warehouse`. Focus only on the solution structure and implementation of Facades and Modules.  
   - In Domain and ReadModel projects, do **not** add abstract classes like `CommandHandlerBaseAsync` or `DomainEventHandlerBaseAsync`.  
   - Do **not** add CommandHandlers or EventHandlers at this stage — they are for later phases.  
   - In the Infrastructure project, limit implementation to `InfrastructureHelper.cs` and `EventStoreSettings`, without additional registration.  

8. Implement **architecture tests** using **NetArchTest**, as shown in `CrastuArrustutu.Carnizzaro.Tests` and `CrastuArrustutu.Tannura.Tests`, to verify proper module isolation.  

9. This phase is considered **DONE** when:  
   - The solution builds (Debug/Release) with no critical warnings (ideally treat warnings as errors).  
   - All architectural tests (NetArchTest) pass.  
   - Base endpoints are exposed: `/v1/Purchase`, `/v1/Warehouse` (even if empty) → HTTP 200/204.  
   - Swagger/OpenAPI is accessible (JSON + Scalar UI) with version info.  
   - Telemetry is configurable (OpenTelemetry module disabled until setup).  
   - README is updated with local run instructions.  

10. **Architectural Test Rules**  
   - No dependencies between modules (e.g., no project in one module may reference a project from another).  
   - `{BrewUp.Rest}` must be the **only** external API entry point.  
   - `{BrewUp.Rest}` must depend only on the Facade projects of the `Purchase`, `Sales` and `Warehouse` modules.  

11. **Naming & Conventions**  
   - Root namespace: `BrewUp.[Module].[Layer]`.  
   - Endpoint group path: `/v1/{module}` (version in path for simplicity).  
   - Module file: `{ModuleName}Module.cs` implements `IModule`.  
   - Facade interfaces: `IPurchaseFacade`, `ISalesFacade`, `IWarehouseFacade` (initially empty).  

12. **API Versioning**  
   - Initial strategy: path-based (`/v1`).  
   - When introducing `/v2`, duplicate only modified endpoints (backward compatible).  
   - OpenAPI Title should include major version (e.g., `BrewUp API v1`).  

13. **OpenAPI & Documentation**  
   - Generate one document per major version.  
   - Base server URL `/` for reverse proxy compatibility.  
   - Tags: use the module name in PascalCase.  
   - Avoid exposing internal domain types — use DTOs in the Facade layer.  

14. **Logging**  
   - Use **Serilog** with Console + File (rolling) sinks, minimum level `Information` (override `Microsoft.*` to `Warning`).  
   - Include trace/span IDs when OpenTelemetry is active.  

15. **Telemetry (OpenTelemetry)**  
   - Required resource attributes: `service.name=BrewUpApi`, `service.version=<assemblyVersion>`.  
   - Enable module by setting `IsEnabled=true`.  
   - Minimum exporters: OTLP (future) + Azure Monitor.  

16. **Error Handling**  
   - Standardize on `ProblemDetails` (RFC 7807).  
   - Map domain exceptions (future) to 409 / 422 as appropriate, with custom code in `problemDetails.Extensions["errorCode"]`.  

17. **Security**  
   - This API will be deployed to **Azure Container Apps**.  
   - Plan authentication and authorization via **Azure AD**.  

18. **Health & Operations**  
   - Add `/healthz` (liveness) and `/ready` (readiness) endpoints (TODO).  
   - OTel metrics automatically available at `/metrics` if Prometheus exporter is added (not required yet).  

19. **Summary**  
   - After generating the new application, provide an evaluation of this prompt based on the resulting outcome.  
