using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class CdCommand : BaseCommand
    {
        public override string Name => "cd";
        public override string Description => "Change current directory";

        public CdCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(string[] args)
        {
            (IEnumerable<string> commandKeys, string source, string destination) = ParseCommandArguments(args);

            // если аргумент не передан — вернуться в "домашний" каталог (например, корень проекта)
            if (string.IsNullOrEmpty(source)) {
                string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                _context.CurrentDirectory = homeDir;
                return new CommandResult { Status = CommandStatus.Success, Message = "" };
            }

            // строим путь относительно текущего контекста
            source = PathNormalize(source);

            if (!Directory.Exists(source)) {
                return new CommandResult { Status = CommandStatus.Error, Message = $"Directory not found: {source}" };
            }

            _context.CurrentDirectory = source;

            return new CommandResult { Status = CommandStatus.Success, Message = "" };
        }
    }
}
