using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class ListCommand : BaseCommand
    {
        public override string Name => "ls";
        public override string Description => "List files and folders in a directory";

        public ListCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(string[] args)
        {
            (IEnumerable<string> flags, string source, string destination) = ParseCommandArguments(args);

            if (string.IsNullOrWhiteSpace(source))
                source = ".";

            source = PathNormalize(source);

            CommandResult commandResult = _fs.List(flags, source);
            return commandResult;
        }
    }
}