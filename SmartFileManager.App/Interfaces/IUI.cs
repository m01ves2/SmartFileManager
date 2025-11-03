using SmartFileManager.Core.Models;

namespace SmartFileManager.App.Interfaces
{
    public interface IUI
    {
        public string ReadInput(string prompt);
        public void WriteOutput(CommandResult commandResult);
    }
}
