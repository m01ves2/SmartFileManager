using SmartFileManager.App.Interfaces;
//using SmartFileManager.CompositionRoot;

namespace SmartFileManager.UI.CLI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ICommandExecutor executor;

            if (args.Contains("--use-container"))
                executor = CompositionRoot.CompositionRootWithContainer.CreateExecutor();
            else
                executor = CompositionRoot.CompositionRoot.CreateExecutor();

            IUI ui = new ConsoleUI(executor);
            ((ConsoleUI)ui).Run();
        }
    }
}