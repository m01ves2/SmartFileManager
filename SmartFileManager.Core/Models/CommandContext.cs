namespace SmartFileManager.Core.Models
{
    public class CommandContext
    {
        public string CurrentDirectory { get; set; } = Directory.GetCurrentDirectory();
        // TODO command history repository
    }
}
