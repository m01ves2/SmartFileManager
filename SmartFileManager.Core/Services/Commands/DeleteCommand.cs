using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class DeleteCommand : BaseCommand
    {
        public override string Name => "rm";
        public override string Description => "Delete file or directory";
        public DeleteCommand(IFileService fileService, IDirectoryService directoryService) : base(fileService, directoryService)
        {
        }

        public override CommandResult Execute(CommandContext context, string[] args)
        {
            try {
                (IEnumerable<string> commandKeys, string source, string destination) = ParseCommandArguments(args);

                if (source == "")
                    return new CommandResult { Status = CommandStatus.Error, Message = "Source path required" };

                if (_fileService.IsFile(source)) {
                    _fileService.DeleteFile(source);
                }
                else if (_directoryService.IsDirectory(source)) {
                    _directoryService.DeleteDirectory(source);
                }
                else {
                    return new CommandResult { Status = CommandStatus.Error, Message = "No such file or directory" };
                }

                return new CommandResult { Status = CommandStatus.Success, Message = $"Deleted {source}" };
            }
            catch (Exception ex) {
                return new CommandResult { Status = CommandStatus.Error, Message = ex.Message };
            }
        }
    }
}
