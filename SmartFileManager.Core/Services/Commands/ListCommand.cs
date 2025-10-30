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

         public ListCommand(IFileService fileService, IDirectoryService directoryService) : base(fileService, directoryService)
        {
        }

        public override CommandResult Execute(CommandContext context, string[] args)
        {
            try {

                (IEnumerable<string> commandKeys, string source, string destination) = ParseArgs(args);
                if (source == "") 
                    source = ".";

                if (_fileService.IsFile(source)) {
                    FileInfo fileInfo = _fileService.GetFileInfo(source);
                    return new CommandResult()
                    {
                        Status = CommandStatus.Success,
                        Message = $"{fileInfo.CreationTime}\t{fileInfo.Attributes}\t{fileInfo.Length}"
                    };
                }
                else if (_directoryService.IsDirectory(source)) {
                    string[] items = _directoryService.ListDirectory(source);
                    StringBuilder sb = new StringBuilder();
                    foreach (string i in items) {
                        sb.Append(i + "\n");
                    }
                    return new CommandResult() { Status = CommandStatus.Success, Message = sb.ToString() };
                }
                else {
                    return new CommandResult() { Status = CommandStatus.Error, Message = "No such file or directory" };
                }

            }
            catch (Exception ex) {
                return new CommandResult { Status = CommandStatus.Error, Message = ex.Message };
            }
        }
    }
}