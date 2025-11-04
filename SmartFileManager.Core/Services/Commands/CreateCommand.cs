using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class CreateCommand : BaseCommand, ICommand
    {

        public override string Name => "mk";
        public override string Description => "Creates file or directory";

        public CreateCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(string[] args)
        {
            (IEnumerable<string> flags, string source, string destination) = ParseCommandArguments(args);

            if ( string.IsNullOrEmpty(source))
                return new CommandResult { Status = CommandStatus.Error, Message = "source path required" };

            source = PathNormalize(source);

            CommandResult commandResult = _fs.Create(flags, source);
            return commandResult;
        }
    }
}
