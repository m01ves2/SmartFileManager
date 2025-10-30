using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Interfaces
{
    public interface IUI
    {
        public string ReadInput(string prompt);
        public void WriteOutput(CommandResult commandResult);
    }
}
