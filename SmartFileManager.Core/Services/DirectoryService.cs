using SmartFileManager.Core.Interfaces;

//low-level working with files/directories
namespace SmartFileManager.Core.Services
{
    public class DirectoryService : IDirectoryService
    {
        public void CreateDirectory(string path)
        {
            try {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot create directory '{path}': {ex.Message}", ex);
            }
        }

        public void DeleteDirectory(string path)
        {
            try {
                Directory.Delete(path, true);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot delete directory '{path}': {ex.Message}", ex);
            }
        }

        public string[] ListDirectory(string path)
        {
            try {
                return Directory.GetFileSystemEntries(path);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot list directory '{path}': {ex.Message}", ex);
            }
        }

        public void CopyDirectory(string source, string destination)
        {
            try {
                Directory.CreateDirectory(destination);
                foreach (var file in Directory.GetFiles(source))
                    File.Copy(file, Path.Combine(destination, Path.GetFileName(file)), true);
                foreach (var dir in Directory.GetDirectories(source))
                    CopyDirectory(dir, Path.Combine(destination, Path.GetFileName(dir)));
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot copy directory '{source}' to '{destination}': {ex.Message}", ex);
            }
        }

        public void MoveDirectory(string source, string destination)
        {
            try {
                Directory.Move(source, destination);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot move directory '{source}' to '{destination}': {ex.Message}", ex);
            }
        }

        public bool IsDirectory(string source)
        {
            return Directory.Exists(source); 
        }
    }
}
