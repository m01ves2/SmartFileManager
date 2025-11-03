using SmartFileManager.App.Interfaces;
using SmartFileManager.App.Services;
using SmartFileManager.Core.Models;

namespace SmartFileManager.UI.CLI
{
    public class ConsoleUI : IUI
    {
        private readonly ICommandExecutor _executor;
        public ConsoleUI(ICommandExecutor executor)
        {
            _executor = executor;
        }

        public void Run()
        {

            while (true) {
                string prompt = _executor.GetPrompt() + "> ";
                string input = ReadInput(prompt);
                //CommandResult commandResult;

                //if (string.IsNullOrEmpty(input)) {
                //    continue;
                //}

                CommandResult commandResult = _executor.Execute(input);
                WriteOutput(commandResult);

                if (commandResult.Status == CommandStatus.Exit) {
                    break;
                }
            }
        }

        public void WriteOutput(CommandResult commandResult)
        {
            if(commandResult.Status == CommandStatus.Success)
                WriteOK(commandResult.Message);
            else if(commandResult.Status == CommandStatus.Error)
                WriteError(commandResult.Message);
            else if(commandResult.Status == CommandStatus.Exit)
                WriteWarning(commandResult.Message);
            else
                WriteWarning($"Unhandled status: {commandResult.Status}");
        }

        public string ReadInput(string prompt)
        {
            Write(prompt);
            string input = (Console.ReadLine() ?? "").Trim();
            return input;
        }

        private void Write(string message)
        { 
            Console.ResetColor();
            Console.Write(message); 
        }
        private void WriteOK(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message); 
            Console.ResetColor();
        }
        private void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

}
