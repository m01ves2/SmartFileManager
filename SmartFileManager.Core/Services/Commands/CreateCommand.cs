using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class CreateCommand : BaseCommand, ICommand
    {

        public override string Name => "mk";
        public override string Description => "Creates file or directory";

        public CreateCommand(IFileSystemService fs) : base(fs)
        {
        }

        public override CommandResult Execute(CommandContext context, string[] args)
        {
            (IEnumerable<string> commandKeys, string source, string destination) = ParseCommandArguments(args);

            if ( string.IsNullOrEmpty(source))
                return new CommandResult { Status = CommandStatus.Error, Message = "source path required" };

            CommandResult commandResult = _fs.Delete(commandKeys, source);
            return commandResult;
        }
    }
}
