# Contributing Guide

Thank you for your interest in SmartFileManager!  
This project is used as a learning platform for:
- Professional Git workflow
- Branch → PR → Review cycles
- Clean architectural principles

---

# 🧩 Git Workflow

This workflow will be used in future projects:

1. Each task → its own branch:
   ```
   feature/<task-name>
   fix/<bug-name>
   refactor/<module>
   ```

2. Branches are created from `main`:
   ```bash
git checkout main
git pull
git checkout -b feature/new-command
```

3. After completion → create a Pull Request:
- Summary of changes
- Testing instructions
- Architectural decisions

4. Reviewer performs code review

5. After approval → merge into main

---

# 🧪 Testing

Unit tests will be added in upcoming phases:
- xUnit
- Unit + integration tests
- Testing commands via TestHost

---

# 📖 Code Style

- Follow Clean Code principles
- Single Responsibility for each class
- Prefer constructor DI (property DI only when required)
- No business logic in UI

---

# 📚 Recommended Practices

- One commit = one logical change
- Commit messages format:

```
feat: add MoveCommand
fix: correct path sanitization
refactor: extract ICommandsAware
```

