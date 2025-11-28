using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SmartFileManager.App.Interfaces;
using SmartFileManager.CompositionRoot;
using System.ComponentModel;

namespace SmartFileManager.UI.CLI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var host = AppHost.Build(args, services =>
            {
                services.AddSingleton<IUI, ConsoleUI>();
            });

            var ui = host.Provider.GetRequiredService<IUI>();

            // ConsoleUI has Run(), but IUI doesn't.
            ((ConsoleUI)ui).Run();
        }
    }
}