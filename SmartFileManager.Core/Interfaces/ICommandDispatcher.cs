using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Interfaces
{
    public interface ICommandDispatcher
    {
        CommandResult Execute(string input);
        string GetCLIPrompt();
    }
}
