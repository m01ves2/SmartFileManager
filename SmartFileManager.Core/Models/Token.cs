namespace SmartFileManager.Core.Models
{
    public class Token
    {
        public string Value { get; }
        public bool IsQuoted { get; } // true если token в кавычках
        public char? QuoteChar { get; } // ' " « » и т.д.

        public Token(string value, bool isQuoted, char? quoteChar)
        {
            Value = value;
            IsQuoted = isQuoted;
            QuoteChar = quoteChar;
        }
    }
}
