namespace SmartFileManager.Core.Interfaces
{
    public interface IItem
    {
        string Path { get; }
        string Name { get; }
        bool Exists();
    }
}
