namespace SmartFileManager.Core.Services
{
    public class CommandParser
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
