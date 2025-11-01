using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;

namespace SmartFileManager.Core.Services.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual bool HideFromHelp => false;

        protected readonly IFileService _fileService;
        protected readonly IDirectoryService _directoryService;

        public BaseCommand(IFileService fileService, IDirectoryService directoryService)
        {
            _fileService = fileService;
            _directoryService = directoryService;
        }
        
        protected (IEnumerable<string> commandKeys, string source, string destination) ParseCommandArguments(string[] args)
        {
            IEnumerable<string> commandKeys = args.Where(t => t.StartsWith('-'));
            var positionalArgs = args.Where(t => !t.StartsWith('-')).ToArray();
            string source = positionalArgs.ElementAtOrDefault(0) ?? "";
            string destination = positionalArgs.ElementAtOrDefault(1) ?? "";

            return (commandKeys, source, destination);
        }

        public abstract CommandResult Execute(CommandContext context, string[] args);
    }
}
