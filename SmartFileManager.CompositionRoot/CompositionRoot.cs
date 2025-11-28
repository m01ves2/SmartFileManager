using Microsoft.Extensions.DependencyInjection;
using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Services.Commands;

namespace SmartFileManager.CompositionRoot
{
    public static class CompositionRoot
    {
        public static void ConfigureCommands(IServiceProvider provider)
        {
            var commands = provider.GetServices<ICommand>().ToList().AsReadOnly();

            foreach (var cmd in commands)
                if (cmd is ICommandsAware aware)
                    aware.SetCommands(commands);
        }
    }
}
