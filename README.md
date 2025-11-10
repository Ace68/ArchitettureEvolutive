# BrewUp API

A .NET 9 Minimal API solution to use as demo for fitness functions, BDD, e2e tests.

## Project Structure

The solution follows a modular architecture with Domain-Driven Design principles:

### Solution Folders
- **90 Presentation**: REST API project
- **80 Infrastructure**: Shared infrastructure services
- **50 Modules**: Bounded contexts (Purchase, Warehouse, Sales)
- **30 Shared**: Shared libraries

### Modules
Each module contains:
- **Facade**: Endpoints and interface exposure
- **Domain**: Business logic and entities
- **Infrastructure**: Data access and repositories  
- **ReadModel**: Query models and handlers
- **SharedKernel**: Module-specific shared types
- **Tests**: Architectural tests using NetArchTest

## Getting Started

### Prerequisites
- .NET 9 SDK
- Visual Studio 2022 or VS Code

### Running the Application

1. Clone the repository
2. Navigate to the src directory:
   ```bash
   cd src
   ```
3. Build the solution:
   ```bash
   dotnet build
   ```
4. Run the tests:
   ```bash
   dotnet test
   ```
5. Start the API:
   ```bash
   dotnet run --project BrewUp.Rest
   ```

### Accessing the API

- **API Base URL**: http://localhost:5064
- **OpenAPI/Swagger**: http://localhost:5064/scalar/v1
- **OpenAPI JSON**: http://localhost:5064/openapi/v1.json

### Available Endpoints

- **GET /v1/purchase**: Purchase module status
- **GET /v1/warehouse**: Warehouse module status  
- **GET /v1/sales**: Sales module status

## Architecture

The solution implements:
- **.NET 9 Minimal APIs** with custom module system
- **OpenAPI documentation** with Scalar UI
- **OpenTelemetry** for observability (disabled by default)
- **Architectural testing** with NetArchTest
- **Module isolation** - no cross-module dependencies

## Configuration

- OpenTelemetry can be enabled by setting `IsEnabled = true` in `OpenTelemetryModule`
- Application Insights connection string should be set in `ConnectionStrings:applicationInsights`

## Development

Each module is independent and can be developed separately. The REST project automatically discovers and registers modules implementing the `IModule` interface.

### Adding New Features
1. Implement business logic in the appropriate Domain project
2. Add data access in Infrastructure project
3. Expose endpoints through Facade project
4. Ensure architectural tests pass