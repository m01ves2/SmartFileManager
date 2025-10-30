using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using SmartFileManager.Core.Services;
using SmartFileManager.UI.Infrastructure;

namespace SmartFileManager.App
{
    public class Program
    {
        private static void Main(string[] args)
        {
            CommandContext commandContext = new CommandContext();
            IFileService fileService = new FileService();
            IDirectoryService directoryService = new DirectoryService();
            CommandRegistry commandRegistry = new CommandRegistry(fileService, directoryService);
            CommandParser commandParser = new CommandParser();

            ICommandHandler commandHandler = new CommandHandler(commandContext, commandRegistry, commandParser);
            IUI uI = new ConsoleUI();

            Coordinator coordinator = new Coordinator(uI, commandHandler);
            coordinator.Start();
        }
    }
}