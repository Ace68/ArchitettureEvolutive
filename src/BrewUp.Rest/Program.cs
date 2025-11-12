using BrewUp.Rest.Modules;

var builder = WebApplication.CreateBuilder(args);

OpenApiModule.Register(builder);
SalesModule.Register(builder);
WarehouseModule.Register(builder);
PurchaseModule.Register(builder);
InfrastructureModule.Register(builder);

var app = builder.Build();

OpenApiModule.Configure(app);
SalesModule.Configure(app);
WarehouseModule.Configure(app);
PurchaseModule.Configure(app);
InfrastructureModule.Configure(app);

await app.RunAsync();
