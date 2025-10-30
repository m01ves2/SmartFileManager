using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services
{
    public class Executor
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IUI _uI;

        public Executor(IUI uI, ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
            _uI = uI;
        }

        public void Start()
        {

            while (true) {
                string prompt = _commandHandler.GetCLIPrompt();
                string input = _uI.ReadInput(prompt);
                CommandResult commandResult;

                if (string.IsNullOrEmpty(input)) {
                    continue;
                }

                commandResult = _commandHandler.Execute(input);

                _uI.WriteOutput(commandResult);

                if (commandResult.Status == CommandStatus.Exit) {
                    break;
                }
            }
        }
    }
}
