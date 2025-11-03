namespace SmartFileManager.App.Interfaces
{
    public interface ICommandParser
    {
        (string commandName, string[] args) Parse(string input);
    }
}
