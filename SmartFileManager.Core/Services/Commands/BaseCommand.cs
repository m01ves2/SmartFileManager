using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual bool HideFromHelp => false;

        protected readonly IFileSystemService _fs;

        public BaseCommand(IFileSystemService fs)
        {
            _fs = fs;
        }

        protected bool IsFile(string path) => File.Exists(path);
        protected bool IsDirectory(string path) => Directory.Exists(path);

        protected (IEnumerable<string> commandKeys, string source, string destination) ParseCommandArguments(string[] args)
        {   
            var positionalArgs = args.Where(t => !t.StartsWith('-')).ToArray();
            string source = positionalArgs.ElementAtOrDefault(0) ?? "";
            string destination = positionalArgs.ElementAtOrDefault(1) ?? "";

            var commandKeys = args.Where(t => t.StartsWith('-')).SelectMany(t => t.Skip(1).Select(c => c.ToString()));
            
            return (commandKeys, source, destination);
        }

        public abstract CommandResult Execute(CommandContext context, string[] args);
    }
}
