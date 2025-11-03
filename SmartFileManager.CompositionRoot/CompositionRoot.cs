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
        public static CommandExecutor CreateExecutor()
        {
            // TODO: Replace with Dependency Injection container after reading "Dependency Injection in .NET"
            
            // Core services
            var commandContext = new CommandContext();
            IFileService fileService = new FileService();
            IDirectoryService directoryService = new DirectoryService();
            IFileSystemService fileSystemService = new FileSystemService(fileService, directoryService);
            IEnumerable<ICommand> commands = BuildCommandList(fileSystemService);

            //App services
            var commandRegistry = new CommandRegistry(commands);
            var commandParser = new CommandParser();
            ICommandDispatcher commandDispatcher = new CommandDispatcher(commandContext, commandRegistry, commandParser);
            CommandExecutor commandExecutor = new CommandExecutor(commandDispatcher);

            return commandExecutor;
        }

        private static List<ICommand> BuildCommandList(IFileSystemService fileSystemService)
        {
            var commands = new List<ICommand>();
            ICommand copyCommand = new CopyCommand(fileSystemService);
            ICommand createCommand = new CreateCommand(fileSystemService);
            ICommand deleteCommand = new DeleteCommand(fileSystemService);
            ICommand listCommand = new ListCommand(fileSystemService);
            ICommand moveCommand = new MoveCommand(fileSystemService);
            ICommand exitCommand = new ExitCommand(fileSystemService);
            ICommand unknownCommand = new UnknownCommand(fileSystemService);

            commands.AddRange(new[] { listCommand, copyCommand, createCommand, deleteCommand, moveCommand, exitCommand, unknownCommand });

            ICommand helpCommand = new HelpCommand(commands, fileSystemService);
            commands.Add(helpCommand);

            return commands;
        }
    }
}
