using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using System.Text;

namespace SmartFileManager.Core.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IFileService _fileService;
        private readonly IDirectoryService _directoryService;

        public FileSystemService(IFileService fileService, IDirectoryService directoryService)
        {
            _fileService = fileService;
            _directoryService = directoryService;
        }

        public CommandResult Copy(IEnumerable<string> commandKeys, string source, string destination)
        {
            if (_fileService.IsFile(source)) {
                _fileService.CopyFile(source, destination); //TODO: flags, e.g. File.Copy(source, destination, overwrite: true);
                return new CommandResult { Status = CommandStatus.Success, Message = $"File copied to {destination}" };
            }
            else if (_directoryService.IsDirectory(source)) {
                _directoryService.CopyDirectory(source, destination); //TODO flags, e.g. Directory.Copy(source, destination, recoursively);
                return new CommandResult { Status = CommandStatus.Success, Message = $"Directory copied to {destination}" };
            }
            else {
                return new CommandResult { Status = CommandStatus.Error, Message = "No such file or directory" };
            }
        }

        public CommandResult Create(IEnumerable<string> commandKeys, string source)
        {
            bool isFile = commandKeys.Count() == 0 || !(commandKeys.Contains("d"));
            if (isFile) {
                _fileService.CreateFile(source);
                return new CommandResult { Status = CommandStatus.Success, Message = $"Created file {source}" };
            }
            else {
                _directoryService.CreateDirectory(source);
                return new CommandResult { Status = CommandStatus.Success, Message = $"Created directory {source}" };
            }
        }

        public CommandResult Delete(IEnumerable<string> commandKeys, string source)
        {
            if (_fileService.IsFile(source)) {
                _fileService.DeleteFile(source);
                return new CommandResult { Status = CommandStatus.Success, Message = $"Deleted file {source}" };
            }
            else if (_directoryService.IsDirectory(source)) {
                _directoryService.DeleteDirectory(source);
                return new CommandResult { Status = CommandStatus.Success, Message = $"Deleted directory {source}" };
            }
            else {
                return new CommandResult { Status = CommandStatus.Error, Message = "No such file or directory" };
            }
        }

        public CommandResult List(IEnumerable<string> commandKeys, string source)
        {
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

        public CommandResult Move(IEnumerable<string> commandKeys, string source, string destination)
        {
            if (_fileService.IsFile(source)) {
                _fileService.MoveFile(source, destination);
                return new CommandResult { Status = CommandStatus.Success, Message = $"File moved to {destination}" };
            }
            else if (_directoryService.IsDirectory(source)) {
                _directoryService.MoveDirectory(source, destination);
                return new CommandResult { Status = CommandStatus.Success, Message = $"Directory moved to {destination}" };
            }
            else {
                return new CommandResult { Status = CommandStatus.Error, Message = "No such file or directory" };
            }
        }
    }
}
