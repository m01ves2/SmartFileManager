using SmartFileManager.Core.Interfaces;

namespace SmartFileManager.Core.Services
{
    public class FileService : IFileService
    {
        public void CreateFile(string path)
        {
            try {
                using (var fs = File.Create(path)) {
                    // File is created!
                }
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot create file '{path}': {ex.Message}", ex);
            }
        }

        public void DeleteFile(string path)
        {
            try {
                File.Delete(path);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot delete file '{path}': {ex.Message}", ex);
            }
        }

        public void CopyFile(string source, string destination)
        {
            try {
                File.Copy(source, destination, true);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot copy file '{source}' to '{destination}': {ex.Message}", ex);
            }
        }

        public void MoveFile(string source, string destination)
        {
            try {
                File.Move(source, destination);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot move file '{source}' to '{destination}': {ex.Message}", ex);
            }
        }

        public FileInfo GetFileInfo(string path)
        {
            try {
                return new FileInfo(path);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot get info for file '{path}': {ex.Message}", ex);
            }
        }

        public bool IsFile(string source)
        {
            return File.Exists(source);
        }
    }
}
