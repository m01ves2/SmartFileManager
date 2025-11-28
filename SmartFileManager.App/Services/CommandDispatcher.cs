using SmartFileManager.App.Interfaces;
using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger; //to shrink Microsoft.Extensions.Logging namespace

namespace SmartFileManager.App.Services
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly CommandContext _commandContext;
        private readonly CommandRegistry _commandRegistry;
        private readonly CommandParser _commandParser;
        private readonly ILogger<CommandDispatcher> _logger;
        public CommandDispatcher(CommandContext commandContext, CommandRegistry commandRegistry, CommandParser commandParser, ILogger<CommandDispatcher> logger)
        {
            _commandContext = commandContext;
            _commandRegistry = commandRegistry;
            _commandParser = commandParser;
            _logger = logger;
        }

        public CommandResult Execute(string input)
        {
            try {
                (string commandName, string[] args) = _commandParser.Parse(input);
                ICommand command = _commandRegistry.GetCommand(commandName);
                CommandResult commandResult = command.Execute(args);
                return commandResult;
            }
            catch (InvalidOperationException ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Error, Message = $"Operation error: {ex.Message}" };
                _logger.LogError($"Operation error: {ex.Message}");
                return commandResult;
            }
            catch (Exception ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Error, Message = $"Unexpected error: {ex.Message}" };
                _logger.LogError($"Operation error: {ex.Message}");
                return commandResult;
            }
        }

        public string GetPrompt()
        {
            return _commandContext.CurrentDirectory;
        }
    }
}