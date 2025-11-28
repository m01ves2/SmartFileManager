# Application Startup & CompositionRoot Pattern

## Purpose
This document describes the standard architecture used for initializing an application:

- dependency injection setup  
- logging  
- host lifecycle  
- composition root  
- UI/backend wiring  

Use this pattern for all console and desktop .NET projects.

---

## Project Structure (recommended)

```
/AppHost
   AppHost.cs
   ServiceCollectionExtensions.cs
   CompositionRoot.cs

/UI
   Program.cs (Main)
   ConsoleUI.cs or WPFUI.cs

/Core
   Services/
   Commands/
   Interfaces/
```

---

## AppHost.cs — Application Entry Wiring (Host Layer)

### Responsibilities

- Configure logging  
- Build the DI container (`IServiceProvider`)  
- Register UI layer  
- Initialize the host runtime  
- Provide the final `IServiceProvider` to the application

### Example Skeleton

```csharp
public class AppHost : IDisposable
{
    private readonly IServiceProvider _provider;

    private AppHost(IServiceProvider provider)
    {
        _provider = provider;
    }

    public static AppHost BuildConsole()
    {
        var services = new ServiceCollection();

        services.AddSmartFileManagerCore();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
        });

        services.AddSingleton<IUI, ConsoleUI>();

        var provider = services.BuildServiceProvider();
        CompositionRoot.Initialize(provider);

        return new AppHost(provider);
    }

    public void Run()
    {
        var ui = _provider.GetRequiredService<IUI>();

        if (ui is ConsoleUI console)
            console.Run();
    }

    public void Dispose() =>
        (_provider as IDisposable)?.Dispose();
}
```

---

## CompositionRoot.cs — Object Graph Finalization

### Responsibilities

- Finalize non-constructor wiring (property injection)  
- Resolve metadata requirements (ICommandsAware, etc)  
- Validate that the graph is consistent

### Example Skeleton

```csharp
public static class CompositionRoot
{
    public static void Initialize(IServiceProvider provider)
    {
        var commands = provider.GetServices<ICommand>().ToList();

        foreach (var command in commands)
        {
            if (command is ICommandsAware aware)
                aware.SetCommands(commands);
        }
    }
}
```

---

## ServiceCollectionExtensions.cs — Registration of Core Services

### Responsibilities

- Provide `AddYourAppNameCore(IServiceCollection)` extension  
- Group registration of:
  - Core services
  - Commands
  - Parsers
  - Dispatchers
  - Application services

### Example Skeleton

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSmartFileManagerCore(this IServiceCollection services)
    {
        services.AddSingleton<IFileSystemService, FileSystemService>();
        services.AddSingleton<CommandRegistry>();
        services.AddSingleton<CommandContext>();
        services.AddSingleton<CommandParser>();
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddSingleton<ICommandExecutor, CommandExecutor>();

        RegisterCommands(services);

        return services;
    }

    private static void RegisterCommands(IServiceCollection services)
    {
        services.AddSingleton<ICommand, CopyCommand>();
        services.AddSingleton<ICommand, HelpCommand>();
        // etc...
    }
}
```

---

## Notes & Best Practices

- Put **all registrations** into extension methods — this keeps the AppHost clean.
- CompositionRoot **should not register DI** — it only completes wiring.
- AppHost controls **startup**, CompositionRoot controls **graph**.

