using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class UnknownCommand : BaseCommand
    {
        public override string Name { get; }
        public override string Description => "Handles unknown commands";

        public UnknownCommand(string name, IFileService fileService, IDirectoryService directoryService) : base(fileService, directoryService)
        {
            Name = name;
        }
        public override CommandResult Execute(CommandContext context, string[] args)
        {
            return new CommandResult() { Status = CommandStatus.Error, Message = $"Unknown command: {Name}. Try 'help'." };
        }
    }
}
