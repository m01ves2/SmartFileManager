using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Services.Commands;

namespace SmartFileManager.Core.Services
{
    public class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _registry;
        //private readonly IFileService _fileService;
        //private readonly IDirectoryService _directoryService;

        public CommandRegistry(IEnumerable<ICommand> commands)
        {
            _registry = commands.ToDictionary(c => c.Name, c => c);
        }

        public ICommand GetCommand(string name)
        {
            if (_registry.ContainsKey(name))
                return _registry[name];
            else
                return _registry["unknown"];
        }
    }
}