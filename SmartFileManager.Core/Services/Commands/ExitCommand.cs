using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class ExitCommand : BaseCommand
    {
        public override string Name => "exit";
        public override string Description => "Close application";

        public ExitCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(string[] args)
        {
            return new CommandResult() { Status = CommandStatus.Exit, Message = "Exiting..." };
        }
    }
}
