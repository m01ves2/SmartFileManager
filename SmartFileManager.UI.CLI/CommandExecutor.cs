using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services
{
    public class CommandExecutor
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IUI _uI;

        public CommandExecutor(IUI uI, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _uI = uI;
        }

        public void Start()
        {

            while (true) {
                string prompt = _commandDispatcher.GetCLIPrompt();
                string input = _uI.ReadInput(prompt);
                CommandResult commandResult;

                if (string.IsNullOrEmpty(input)) {
                    continue;
                }

                commandResult = _commandDispatcher.Execute(input);

                _uI.WriteOutput(commandResult);

                if (commandResult.Status == CommandStatus.Exit) {
                    break;
                }
            }
        }
    }
}
