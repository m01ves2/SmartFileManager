using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Interfaces
{
    public interface IFileSystemService
    {
        CommandResult Copy(IEnumerable<string> commandKeys, string source, string destination);
        CommandResult Delete(IEnumerable<string> commandKeys, string source);
        CommandResult Create(IEnumerable<string> commandKeys, string source);
        CommandResult Move(IEnumerable<string> commandKeys, string source, string destination);
        CommandResult List(IEnumerable<string> commandKeys, string source);

    }
}
