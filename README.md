SmartFileManager is an educational yet architecturally correct file management application built using Clean Architecture principles, Dependency Injection, and modular project structure.

This project serves as a platform for learning:
- Microsoft.Extensions.DependencyInjection
- Composition Root
- Application Hosting
- Modular command system design
- Layered architecture in .NET

---

## 🚀 Features

- Directory navigation (`cd`)
- Directory listing (`list`)
- Copy, move, delete files
- File creation
- `help` command — displays all available commands
- Colored console output (ConsoleUI)
- Logging (console + file)

---

## 📂 Project Structure
```
SmartFileManager/
│
├── AppHost/
│     ├── AppHost.cs
│     ├── ServiceCollectionExtensions.cs
│     └── CompositionRoot.cs
│
├── Core/
│     ├── Interfaces/
│     ├── Services/
│     └── Commands/
│
├── UI/
│     ├── ConsoleUI.cs
│     └── Program.cs
│
└── README.md
```

---

## ▶ Running the Application

```
dotnet run --project SmartFileManager.UI.CLI
```

---

## 🛠 Technologies Used

- .NET 8
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Logging
- Serilog
- Clean Architecture

---

## 📌 Documentation

- [ARCHITECTURE.md](./ARCHITECTURE.md)
- [CONTRIBUTING.md](./CONTRIBUTING.md)

---

## 📅 Planned Features

- Natural language command parsing
- Command completion / prediction
- Integration with an external REST API
- Enhanced UI output (colors, inline help)
- WPF UI version
- Unit tests (xUnit)

---

## 📜 License

MIT (educational project)

---