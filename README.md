## Injectify
Injectify is a lightweight, flexible Dependency Injection (DI) container for .NET applications. It provides a  powerful way to manage dependencies in your projects, supporting constructor injection, property injection, and method injection.

### Features
# 1. Multiple Injection Types
  - Constructor Injection
  - Property Injection
  - Method Injection

# 2. Lifetime Management
  - Transient: A new instance is created each time it's requested
  - Singleton: A single instance is created and reused for all requests
  - Scoped: A single instance is created per scope (useful for web applications)


# 3. Flexible Registration
  - Generic registration
  - Type-based registration


# 4. Scoped Resolution
  - Create and manage scopes for more granular control over object lifetimes


# 5.Automatic Dependency Resolution
 - Recursively resolves nested dependencies


# Thread-Safe
Uses ConcurrentDictionary for thread-safe operations



## Usage
# Basic Setup
```csharp
var container = new Container();
```

# Registration
```csharp
// Register a transient service
container.Register<IService, ServiceImplementation>();

// Register a singleton service
container.Register<IRepository, RepositoryImplementation>(LifeTime.Singleton);

// Register a scoped service
container.Register<IUnitOfWork, UnitOfWork>(LifeTime.Scoped);

```
# Resolution
```csharp
// Resolve a service
var service = container.Resolve<IService>();

// Resolve a type
var repository = container.Resolve(typeof(IRepository));
```
# Property Injection
```csharp
public class MyClass
{
    [Inject]
    public IService Service { get; set; }
}

var instance = container.Resolve<MyClass>();
// The Service property will be automatically injected
```
# Method Injection
```csharp
public class MyClass
{
    [Inject]
    public void Initialize(IService service)
    {
        // Method will be called with resolved IService
    }
}

var instance = container.Resolve<MyClass>();
// The Initialize method will be automatically called with resolved dependencies
```
# Scopes
```csharp
using (var scope = container.CreateScope())
{
    var scopedService = scope.Resolve<IScopedService>();
    // Use scoped service
}
// Scoped services are disposed when the scope is disposed
```


## Best Practices

1. Register dependencies at the composition root of your application.
2. Prefer constructor injection for required dependencies.
3. Use property injection for optional dependencies.
4. Be cautious with circular dependencies.
5. Dispose of scopes when you're done with them to release resources.

## Future Features

# 1. Enhanced Generic Type Resolution
```csharp
container.Register(typeof(IRepository<>), typeof(GenericRepository<>));
var userRepo = container.Resolve<IRepository<User>>();
```

# 2. Decorator Pattern Support
```csharp
container.Register<IService, ConcreteService>();
container.RegisterDecorator<IService, LoggingDecorator>();
```
# 3. Lazy Resolution
```csharp
var lazyService = container.ResolveLazy<IExpensiveService>();
// Service is only created when Value is accessed
var actualService = lazyService.Value;
```



