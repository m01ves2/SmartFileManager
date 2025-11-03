using SmartFileManager.App.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.App.Services
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly ICommandDispatcher _dispatcher;

        public CommandExecutor(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public string GetPrompt()
        {
            return _dispatcher.GetPrompt();
        }

        public CommandResult Execute(string input)
        {
            return _dispatcher.Execute(input);
        }
    }
}
