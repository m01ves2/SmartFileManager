using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using System.Text;

namespace SmartFileManager.Core.Services.Commands
{
    //команды не являются сущностями, но живут в Core, потому что реализуют бизнес-логику работы с сущностями
    public class ListCommand : BaseCommand
    {
        public override string Name => "ls";
        public override string Description => "List files and folders in a directory";

        public ListCommand(IFileSystemService fs) : base(fs)
        {
        }

        public override CommandResult Execute(CommandContext context, string[] args)
        {
            (IEnumerable<string> commandKeys, string source, string destination) = ParseCommandArguments(args);
            if (string.IsNullOrEmpty(source))
                source = ".";

            CommandResult commandResult = _fs.List(commandKeys, source);
            return commandResult;
        }
    }
}