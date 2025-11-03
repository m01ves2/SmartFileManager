using SmartFileManager.Core.Models;

namespace SmartFileManager.App.Interfaces
{
    public interface IFormatter
    {
        string Format(CommandResult result);
    }
}