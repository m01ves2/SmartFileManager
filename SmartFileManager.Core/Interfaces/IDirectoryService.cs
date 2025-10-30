namespace SmartFileManager.Core.Interfaces
{
    public interface IDirectoryService
    {
        string[] ListDirectory(string path);
        void CreateDirectory(string path);
        void DeleteDirectory(string path);
        void CopyDirectory(string source, string destination);
        void MoveDirectory(string source, string destination);
        bool IsDirectory(string source);
    }
}
