using SmartFileManager.Core.Interfaces;

namespace SmartFileManager.Core.Models
{
    public class DirectoryItem : IItem
    {
        public string Path { get; }
        public string Name => System.IO.Path.GetFileName(Path);

        public DirectoryItem(string path) => Path = path;
        public bool Exists() => Directory.Exists(Path);
        
        // Ленивая загрузка содержимого
        public List<IItem> GetItems()
        {
            var dirs = Directory.GetDirectories(Path).Select(p => new DirectoryItem(p));
            var files = Directory.GetFiles(Path).Select(p => new FileItem(p));
            return dirs.Cast<IItem>().Concat(files.Cast<IItem>()).ToList();
        }
    }
}
