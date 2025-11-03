using SmartFileManager.App.Interfaces;

namespace SmartFileManager.UI.CLI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Main = start point
            ICommandExecutor commandExecutor = CompositionRoot.CompositionRoot.CreateExecutor();
            
            // UI & Formatter
            IFormatter formatter = new ConsoleFormatter(); // если понадобится позже
            IUI ui = new ConsoleUI(commandExecutor);

            ((ConsoleUI)ui).Run();
        }
    }
}