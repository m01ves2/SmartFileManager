using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SmartFileManager.App.Interfaces;

namespace SmartFileManager.CompositionRoot
{
    public sealed class AppHost
    {
        public IServiceProvider Provider { get; }

        private AppHost(IServiceProvider provider)
        {
            Provider = provider;
        }

        public static AppHost Build(string[] args, Action<IServiceCollection> configureUI)
        {
            // Создаём Serilog вручную (root-logger)
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var services = new ServiceCollection();

            // Logging
            services.AddLogging(b =>
            {
                b.AddSerilog(logger, dispose: true);
            });

            // Core + Commands + App services
            services.AddSmartFileManagerCore();

            // UI (настраивает хост-приложение, не композиционный слой!)
            configureUI?.Invoke(services);

            var provider = services.BuildServiceProvider();

            // Final wiring of HelpCommand etc.
            CompositionRoot.ConfigureCommands(provider);

            return new AppHost(provider);
        }

    }
}
