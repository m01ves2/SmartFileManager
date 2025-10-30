using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Interfaces
{
    public interface ICommand
    {
        public string Name { get; }
        public string Description { get; }
        CommandResult Execute(CommandContext context, string[] args);
    }
}
