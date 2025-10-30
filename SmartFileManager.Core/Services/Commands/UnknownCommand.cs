using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class UnknownCommand : BaseCommand
    {
        public override string Name => "unknown";
        public override string Description => "Handles unknown commands";
        public override bool HideFromHelp => true;

        public UnknownCommand(IFileService fileService, IDirectoryService directoryService) : base(fileService, directoryService)
        {
        }
        public override CommandResult Execute(CommandContext context, string[] args)
        {
            return new CommandResult() { Status = CommandStatus.Error, Message = $"Unknown command. Try 'help'." };
        }
    }
}
