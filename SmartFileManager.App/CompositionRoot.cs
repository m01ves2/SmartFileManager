using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using SmartFileManager.Core.Services;
using SmartFileManager.Core.Services.Commands;
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
            IEnumerable<ICommand> commands = BuildCommandList(fileService, directoryService);
            var commandRegistry = new CommandRegistry(commands);
            var commandParser = new CommandParser();
            ICommandHandler commandHandler = new CommandHandler(commandContext, commandRegistry, commandParser);

            // UI & Formatter
            IUI ui = new ConsoleUI();
            IFormatter formatter = new ConsoleFormatter(); // если понадобится позже

            // Executor (ранее Coordinator)
            return new Executor(ui, commandHandler);
        }

        private static List<ICommand> BuildCommandList(IFileService fileService, IDirectoryService directoryService)
        {
            var commands = new List<ICommand>();
            ICommand copyCommand = new CopyCommand(fileService, directoryService);
            ICommand createCommand = new CreateCommand(fileService, directoryService);
            ICommand deleteCommand = new DeleteCommand(fileService, directoryService);
            ICommand listCommand = new ListCommand(fileService, directoryService);
            ICommand moveCommand = new MoveCommand(fileService, directoryService);
            ICommand exitCommand = new ExitCommand(fileService, directoryService);
            ICommand unknownCommand = new UnknownCommand(fileService, directoryService);

            commands.AddRange(new[] { listCommand, copyCommand, createCommand, deleteCommand, moveCommand, exitCommand, unknownCommand });

            ICommand helpCommand = new HelpCommand(commands, fileService, directoryService);
            commands.Add(helpCommand);

            return commands;
        }
    }
}
