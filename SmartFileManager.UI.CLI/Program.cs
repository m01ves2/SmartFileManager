using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SmartFileManager.App.Interfaces;
using System.ComponentModel;

namespace SmartFileManager.UI.CLI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var serilogLogger = new LoggerConfiguration()
                                .WriteTo.Console()
                                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                                .CreateLogger();

            var provider = CompositionRoot.CompositionRoot.BuildProvider(services =>
            {
                // Логгирование
                services.AddLogging(builder =>
                {
                    builder.AddSerilog(logger: serilogLogger, dispose: true);
            });

                // UI
                services.AddSingleton<IUI, ConsoleUI>();
            });

            var ui = provider.GetRequiredService<IUI>();
            ((ConsoleUI)ui).Run();
        }
    }
}