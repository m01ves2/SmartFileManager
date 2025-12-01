using SmartFileManager.Core.Models;

namespace SmartFileManager.App.Interfaces
{
    public interface ITokenizer
    {
        IReadOnlyList<Token> Tokenize(string input);
    }
}
