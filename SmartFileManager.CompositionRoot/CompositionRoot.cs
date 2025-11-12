using SmartFileManager.App.Interfaces;
using SmartFileManager.App.Services;
using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using SmartFileManager.Core.Services;
using SmartFileManager.Core.Services.Commands;

namespace SmartFileManager.CompositionRoot
{
    public static class CompositionRoot
    {
        public static ICommandExecutor CreateExecutor()
        {           
            // Core services
            var commandContext = new CommandContext();
            IFileService fileService = new FileService();
            IDirectoryService directoryService = new DirectoryService();
            IFileSystemService fileSystemService = new FileSystemService(fileService, directoryService);
            IEnumerable<ICommand> commands = CreateCommands(fileSystemService, commandContext);

            //App services
            var commandRegistry = new CommandRegistry(commands);
            var commandParser = new CommandParser();
            ICommandDispatcher commandDispatcher = new CommandDispatcher(commandContext, commandRegistry, commandParser);
            CommandExecutor commandExecutor = new CommandExecutor(commandDispatcher);

            return commandExecutor;
        }

        private static List<ICommand> CreateCommands(IFileSystemService fileSystemService, CommandContext context)
        {
            var commands = new List<ICommand>();
            ICommand copyCommand = new CopyCommand(fileSystemService, context);
            ICommand createCommand = new CreateCommand(fileSystemService, context);
            ICommand deleteCommand = new DeleteCommand(fileSystemService, context);
            ICommand listCommand = new ListCommand(fileSystemService, context);
            ICommand moveCommand = new MoveCommand(fileSystemService, context);
            ICommand exitCommand = new ExitCommand(fileSystemService, context);
            ICommand cdCommand = new CdCommand(fileSystemService, context);
            ICommand unknownCommand = new UnknownCommand(fileSystemService, context);

            commands.AddRange(new[] { listCommand, copyCommand, createCommand, deleteCommand, moveCommand, exitCommand, cdCommand, unknownCommand });

            ICommand helpCommand = new HelpCommand(commands, fileSystemService, context);
            commands.Add(helpCommand);

            return commands;
        }
    }
}
