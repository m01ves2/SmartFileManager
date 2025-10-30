using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class CreateCommand : BaseCommand, ICommand
    {

        public override string Name => "mk";
        public override string Description => "Creates file or directory";

        public CreateCommand(IFileService fileService, IDirectoryService directoryService) : base(fileService, directoryService)
        {
        }

        public override CommandResult Execute(CommandContext context, string[] args)
        {
            try {
                (IEnumerable<string> commandKeys, string source, string destination) = ParseArgs(args);

                if (source == "")
                    return new CommandResult { Status = CommandStatus.Error, Message = "Source path required" };

                bool isFile = commandKeys.Count() == 0 || !(commandKeys.Contains("-d") || (commandKeys.ElementAtOrDefault(0)?? "").Contains('d'));
                if (isFile) {
                    _fileService.CreateFile(source);
                }
                else {
                    _directoryService.CreateDirectory(source);
                }

                return new CommandResult { Status = CommandStatus.Success, Message = $"Created {source}" };
            }
            catch (Exception ex) {
                return new CommandResult { Status = CommandStatus.Error, Message = ex.Message };
            }

        }
    }
}
