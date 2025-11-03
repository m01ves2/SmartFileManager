using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class ExitCommand : BaseCommand
    {
        public override string Name => "exit";
        public override string Description => "Close application";

        public ExitCommand(IFileSystemService fs) : base(fs)
        {
        }

        public override CommandResult Execute(CommandContext context, string[] args)
        {
            return new CommandResult() { Status = CommandStatus.Exit, Message = "Exiting..." };
        }
    }
}
