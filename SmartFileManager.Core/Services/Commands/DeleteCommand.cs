using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class DeleteCommand : BaseCommand
    {
        public override string Name => "rm";
        public override string Description => "Delete file or directory";
        public DeleteCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(string[] args)
        {
            (IEnumerable<string> commandKeys, string source, string destination) = ParseCommandArguments(args);

            if (string.IsNullOrEmpty(source))
                return new CommandResult { Status = CommandStatus.Error, Message = "source path required" };
            
            source = PathNormalize(source);
            
            CommandResult commandResult = _fs.Delete(commandKeys, source);
            return commandResult;
        }
    }
}
