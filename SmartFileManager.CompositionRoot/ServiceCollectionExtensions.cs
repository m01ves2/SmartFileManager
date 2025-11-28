using Microsoft.Extensions.DependencyInjection;
using SmartFileManager.App.Interfaces;
using SmartFileManager.App.Services;
using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using SmartFileManager.Core.Services.Commands;
using SmartFileManager.Infrastructure.Services;

namespace SmartFileManager.CompositionRoot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmartFileManagerCore(this IServiceCollection services)
        {
            // Core services
            services.AddSingleton<IFileSystemService, FileSystemService>();
            services.AddSingleton<CommandContext>();
            services.AddSingleton<CommandRegistry>();
            services.AddSingleton<IDirectoryService, DirectoryService>();
            services.AddSingleton<IFileService, FileService>();

            // Commands
            RegisterCommands(services);

            //App services
            services.AddSingleton<CommandRegistry, CommandRegistry>();
            services.AddSingleton<CommandParser, CommandParser>();
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<ICommandExecutor, CommandExecutor>();

            return services;
        }

        private static void RegisterCommands(IServiceCollection services)
        {
            services.AddSingleton<ICommand, CopyCommand>();
            services.AddSingleton<ICommand, CreateCommand>();
            services.AddSingleton<ICommand, DeleteCommand>();
            services.AddSingleton<ICommand, ListCommand>();
            services.AddSingleton<ICommand, MoveCommand>();
            services.AddSingleton<ICommand, ExitCommand>();
            services.AddSingleton<ICommand, CdCommand>();
            services.AddSingleton<ICommand, UnknownCommand>();
            services.AddSingleton<ICommand, HelpCommand>();
        }
    }

}
