using SmartFileManager.App.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.UI.CLI
{
    public class ConsoleFormatter : IFormatter
    {
        public string Format(CommandResult result)
        {
            return result.Status == CommandStatus.Success ? $"[OK] {result.Message}" : $"[ERROR] {result.Message}";
        }
    }
}
