using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using SmartFileManager.Core.Services;
using SmartFileManager.UI.CLI;
using SmartFileManager.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFileManager.App
{
    internal static class CompositionRoot
    {
        public static Executor CreateExecutor()
        {
            // Core services
            var commandContext = new CommandContext();
            IFileService fileService = new FileService();
            IDirectoryService directoryService = new DirectoryService();
            var commandRegistry = new CommandRegistry(fileService, directoryService);
            var commandParser = new CommandParser();
            ICommandHandler commandHandler = new CommandHandler(commandContext, commandRegistry, commandParser);

            // UI & Formatter
            IUI ui = new ConsoleUI();
            IFormatter formatter = new ConsoleFormatter(); // если понадобится позже

            // Executor (ранее Coordinator)
            return new Executor(ui, commandHandler);
        }
    }
}
