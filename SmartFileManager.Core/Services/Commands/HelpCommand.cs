using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using System.Text;

namespace SmartFileManager.Core.Services.Commands
{
    public class HelpCommand : BaseCommand, ICommandsAware
    {
        public override string Name => "help";
        public override string Description => "Displays a list of available commands.";
        
        private IReadOnlyList<ICommand> _commands = [];

        public HelpCommand(IFileSystemService fs, CommandContext commandContext) : base(fs, commandContext)
        {
        }

        public override CommandResult Execute(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--help: \n");
            foreach (var item in _commands.Where(c => c.HideFromHelp == false)) {
                sb.Append($"{item.Name} - {item.Description}" + "\n");
            }
            return new CommandResult() { Status = CommandStatus.Success, Message = sb.ToString() };
        }

        public void SetCommands(IReadOnlyList<ICommand> commands)
        {
            _commands = commands;
        }
    }
}
