using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class MoveCommand : BaseCommand
    {
        public override string Name => "mv";
        public override string Description => "Move file or directory";
        public MoveCommand(IFileSystemService fs) : base(fs)
        {
        }
        public override CommandResult Execute(CommandContext context, string[] args)
        {
            (IEnumerable<string> commandKeys, string source, string destination) = ParseCommandArguments(args);

            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(destination))
                return new CommandResult { Status = CommandStatus.Error, Message = "source and destination paths required" };

            CommandResult commandResult = _fs.Move(commandKeys, source, destination);
            return commandResult;
        }
    }
}
