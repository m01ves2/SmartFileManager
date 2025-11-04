using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class MoveCommand : BaseCommand
    {
        public override string Name => "mv";
        public override string Description => "Move file or directory";
        public MoveCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }
        public override CommandResult Execute(string[] args)
        {
            (IEnumerable<string> flags, string source, string destination) = ParseCommandArguments(args);

            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(destination))
                return new CommandResult { Status = CommandStatus.Error, Message = "source and destination paths required" };
            
            source = PathNormalize(source);
            destination = PathNormalize(destination);

            CommandResult commandResult = _fs.Move(flags, source, destination);
            return commandResult;
        }
    }
}
