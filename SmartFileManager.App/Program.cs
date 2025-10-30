using SmartFileManager.Core.Interfaces;
using SmartFileManager.Core.Models;
using SmartFileManager.Core.Services;
using SmartFileManager.UI.Infrastructure;

namespace SmartFileManager.App
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Main только запускает приложение
            var executor = CompositionRoot.CreateExecutor();
            executor.Start();
        }
    }
}