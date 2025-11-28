//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Serilog;
//using SmartFileManager.App.Interfaces;
//using SmartFileManager.App.Services;
//using SmartFileManager.Core.Interfaces;
//using SmartFileManager.Core.Models;
//using SmartFileManager.Core.Services.Commands;
//using SmartFileManager.Infrastructure.Services;
//using SmartFileManager.UI.CLI;

using Microsoft.Extensions.DependencyInjection;
using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Services.Commands;

namespace SmartFileManager.CompositionRoot
{
    public static class CompositionRoot
    {
        public static IServiceProvider BuildProvider(Action<IServiceCollection> configureUI)
        {
            var services = new ServiceCollection();

            // Регистрация ядра и команд
            services.AddSmartFileManagerCore();

            // UI-регистрация передаётся из хоста
            configureUI(services);

            var provider = services.BuildServiceProvider();
            ConfigureCommands(provider);

            return provider;
        }

        private static void ConfigureCommands(IServiceProvider provider)
        {
            var commands = provider.GetServices<ICommand>().ToList().AsReadOnly();

            foreach (var cmd in commands)
                if (cmd is ICommandsAware aware)
                    aware.SetCommands(commands);
        }
    }
}
