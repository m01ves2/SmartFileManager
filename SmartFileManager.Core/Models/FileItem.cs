using SmartFileManager.Core.Interfaces;

namespace SmartFileManager.Core.Models
{
    public class FileItem : IItem
    {
        public string Path { get; }
        public string Name => System.IO.Path.GetFileName(Path);

        public FileItem(string path) => Path = path;
        public bool Exists() => File.Exists(Path);
    }
}
