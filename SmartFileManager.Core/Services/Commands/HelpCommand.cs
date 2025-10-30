using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using System.Text;

namespace SmartFileManager.Core.Services.Commands
{
    public class HelpCommand : BaseCommand
    {
        public override string Name => "help";
        public override string Description => "Displays a list of available commands.";

        private IReadOnlyList<ICommand> _commands;

        public HelpCommand(IReadOnlyList<ICommand> commands, IFileService fileService, IDirectoryService directoryService) : base(fileService, directoryService)
        {
            _commands = commands;
        }

        public override CommandResult Execute(CommandContext context, string[] args)
        {          
            StringBuilder sb = new StringBuilder();
            sb.Append("--help: \n");
            foreach (var item in _commands.Where(c => c.HideFromHelp == false)) {
                sb.Append(item.Name + " - " + item.Description + "\n");
            }
            return new CommandResult() { Status = CommandStatus.Success, Message = sb.ToString() };
        }
    }
}
