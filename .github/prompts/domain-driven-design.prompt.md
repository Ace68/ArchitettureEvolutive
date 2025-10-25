# Domain-Driven Design (DDD) Instructions

## General Principles
- Apply Domain-Driven Design patterns rigorously throughout all generated code  
- Maintain a clear separation between architectural layers (Domain, Application, Infrastructure, Presentation)  
- Use the domain’s **Ubiquitous Language** for class, method, and property names  

## Strategic Design
- Each **Bounded Context** ({ModuleName}) must be completely isolated from the others  
- Do **not** create direct dependencies between different Bounded Contexts  
- Use **Facade interfaces** to expose only the necessary functionality to the outside  
- Maintain the **Context Map** through the Facade interfaces  

## Tactical Design Patterns
- **Entities**: Must have a unique and persistent identity; implement equality based on the ID  
- **Value Objects**: Must be immutable; implement equality based on values; have no identity  
- **Aggregates**: Clearly define the Aggregate Root; maintain data invariants; access only through the root  
- **Domain Services**: For domain logic that doesn’t belong to a single entity  
- **Repositories**: Only interfaces in the Domain layer; implementations go in Infrastructure  
- **Domain Events**: Used to communicate significant changes within the domain  

## Layer Structure
- **Domain Layer**: Contains only entities, value objects, domain services, repository interfaces, and domain events. No external dependencies  
- **Application Layer**: Contains use cases, command/query handlers, and application services. Depends only on the Domain layer  
- **Infrastructure Layer**: Contains concrete implementations of repositories, external services, and persistence mechanisms  
- **Facade Layer**: Exposes endpoints and coordinates application services. Contains no business logic  

## Dependency Rules
- The **Domain layer** must not depend on any other layer  
- The **Application layer** may depend only on the Domain layer  
- The **Infrastructure layer** implements the interfaces defined in the Domain layer  
- The **Facade layer** may depend on Application and Domain layers, but never directly on Infrastructure  

## Naming Conventions
- Use the **domain language**: `Meat`, `Skewer`, `Cooking`, `Order`  
- Entities should have names that reflect business concepts  
- Services should have names that describe domain actions  
- Events should use the **past tense**: `MeatAddedToSkewer`, `CookingCompleted`
