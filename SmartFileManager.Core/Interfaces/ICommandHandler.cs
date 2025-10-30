using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Interfaces
{
    public interface ICommandHandler
    {
        CommandResult Execute(string input);
        string GetCLIPrompt();
    }
}
