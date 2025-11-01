using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class MoveCommand : BaseCommand
    {
        public override string Name => "mv";
        public override string Description => "Move file or directory";
        public MoveCommand(IFileService fileService, IDirectoryService directoryService) : base(fileService, directoryService)
        {
        }
        public override CommandResult Execute(CommandContext context, string[] args)
        {
            try {
                (IEnumerable<string> keysCommand, string source, string destination) = ParseCommandArguments(args);

                if (source == "" || destination == "")
                    return new CommandResult { Status = CommandStatus.Error, Message = "Source and Destination paths required" };

                if (_fileService.IsFile(source)) {
                    _fileService.MoveFile(source, destination);
                }
                else if (_directoryService.IsDirectory(source)) {
                    _directoryService.MoveDirectory(source, destination);
                }
                else {
                    return new CommandResult { Status = CommandStatus.Error, Message = "No such file or directory" };
                }

                    return new CommandResult { Status = CommandStatus.Success, Message = $"Moved to {destination}" };
            }
            catch (Exception ex) {
                return new CommandResult { Status = CommandStatus.Error, Message = ex.Message };
            }
        }
    }
}
