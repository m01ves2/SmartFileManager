using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Services.Commands;

namespace SmartFileManager.Core.Services
{
    public class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _registry;
        private readonly IFileService _fileService;
        private readonly IDirectoryService _directoryService;
        public CommandRegistry(IFileService fileService, IDirectoryService directoryService) 
        {
            _fileService = fileService;
            _directoryService = directoryService;

            var commands = new List<ICommand>();

            ICommand copyCommand = new CopyCommand(_fileService, _directoryService);
            ICommand createCommand = new CreateCommand(_fileService, _directoryService);
            ICommand deleteCommand = new DeleteCommand(_fileService, _directoryService);
            ICommand listCommand = new ListCommand(_fileService, _directoryService);
            ICommand moveCommand = new MoveCommand(_fileService, _directoryService);
            ICommand exitCommand = new ExitCommand(_fileService, _directoryService);

            commands.AddRange(new ICommand[] { 
                listCommand,
                copyCommand,
                createCommand,
                deleteCommand,
                moveCommand,
                exitCommand
            });

            ICommand helpCommand = new HelpCommand(commands, _fileService, _directoryService); // HelpCommand получает уже готовую коллекцию
            commands.Add(helpCommand);

            if (commands.Select(c => c.Name).Distinct().Count() != commands.Count)
                throw new InvalidOperationException("Duplicate command names detected.");

            // а потом — уже реестр
            _registry = commands.ToDictionary(c => c.Name, c => c);
        }

        public ICommand GetCommand(string name)
        {
            if (_registry.ContainsKey(name))
                return _registry[name];
            else
                return new UnknownCommand(name, _fileService, _directoryService);
        }
    }
}