using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services
{
    public class CommandHandler : ICommandHandler
    {
        private readonly CommandContext _commandContext;
        private readonly CommandRegistry _commandRegistry;
        private readonly CommandParser _commandParser;
        public CommandHandler(CommandContext commandContext, CommandRegistry commandRegistry, CommandParser commandParser)
        {
            _commandContext = commandContext;
            _commandRegistry = commandRegistry;
            _commandParser = commandParser;
        }

        public CommandResult Execute(string input)
        {
            try {
                (string commandName, string[] args) = _commandParser.Parse(input);
                ICommand command = _commandRegistry.GetCommand(commandName);
                return command.Execute(_commandContext, args);
            }
            catch (InvalidOperationException ex) {
                LogError("Operation", ex);
                return new CommandResult() { Status = CommandStatus.Error, Message = $"Operation error: {ex.Message}"};
            }
            catch (Exception ex) {
                LogError("Unexpected", ex);
                return new CommandResult() { Status = CommandStatus.Error, Message = $"Unexpected error: {ex.Message}"};
            }
        }

        public string GetCLIPrompt()
        {
            return _commandContext.CurrentDirectory + ">";
        }

        private void LogError(string category, Exception ex)
        {
            try {
                string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                Directory.CreateDirectory(logDir); // create directory if it doesn't exist
                string logFile = Path.Combine(logDir, "errors.txt");

                string logEntry =
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{category}] {ex.Message}\n" +
                    $"{ex.StackTrace}\n\n";

                File.AppendAllText(logFile, logEntry + Environment.NewLine);
            }
            catch {
                // no Exceptions to outside. "Silent logging"
            }
        }
    }
}