using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Interfaces
{
    public interface IFormatter
    {
        string Format(CommandResult result);
    }
}