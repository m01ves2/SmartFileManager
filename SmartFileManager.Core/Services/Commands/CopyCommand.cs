using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public class CopyCommand : BaseCommand
    {
        public override string Name => "cp";
        public override string Description => "Copy file or directory";

        public CopyCommand(IFileService fileService, IDirectoryService directoryService) : base(fileService, directoryService)
        {
        }

        public override CommandResult Execute(CommandContext context, string[] args)
        {
            try {
                (IEnumerable<string> keysCommand, string source, string destination) = ParseArgs(args);
                
                if (source == "" || destination == "")
                    return new CommandResult { Status = CommandStatus.Error, Message = "Source and Destination paths required" };

                if (_fileService.IsFile(source)) {
                    _fileService.CopyFile(source, destination); //TODO: flags, e.g. File.Copy(source, destination, overwrite: true);
                }
                else if(_directoryService.IsDirectory(source)) {
                    _directoryService.CopyDirectory(source, destination); //TODO flags, e.g. Directory.Copy(source, destination, recoursively);
                }
                else {
                    return new CommandResult { Status = CommandStatus.Error, Message = "No such file or directory" };
                }

                    return new CommandResult { Status = CommandStatus.Success, Message = $"Copied to {destination}" };
            }
            catch (Exception ex) {
                return new CommandResult { Status = CommandStatus.Error, Message = ex.Message };
            }
        }
    }
}
