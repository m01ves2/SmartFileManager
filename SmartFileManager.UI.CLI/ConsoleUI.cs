using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.UI.Infrastructure
{
    //вывод текста, ошибок, меню - создаёт команды из текста (связывает App и Core)
    public class ConsoleUI : IUI
    {
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
