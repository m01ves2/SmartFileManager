using SmartFileManager.App.Interfaces;

namespace SmartFileManager.App.Services
{
    public class CommandParser : ICommandParser
    {
        public (string commandName, string[] args) Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return ("", Array.Empty<string>());

            var tokens = input.Trim().Split(' ');
            string commandName = tokens[0].ToLowerInvariant();
            string[] args = tokens.Skip(1).ToArray();

            return (commandName, args);
        }
    }
}
