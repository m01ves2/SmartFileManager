using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Services.Commands;

namespace SmartFileManager.Core.Services
{
    /// <summary>
    /// Registry for Command metadata & lookup
    /// </summary>
    public class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _registry;
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