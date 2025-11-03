using SmartFileManager.Core.Models;

namespace SmartFileManager.App.Interfaces
{
    public interface ICommandExecutor
    {
        CommandResult Execute(string input);
        string GetPrompt();
    }
}
