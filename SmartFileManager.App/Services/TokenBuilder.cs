using SmartFileManager.Core.Models;
using System.Text;

namespace SmartFileManager.App.Services
{
    class TokenBuilder
    {
        private StringBuilder _sb = new StringBuilder();
        public bool IsQuoted { get; set; }
        public char? QuoteChar { get; set; }

        public void Append(char c)
        {
            _sb.Append(c);
        }

        public Token Build()
        {
            string value = IsQuoted ? _sb.ToString() : _sb.ToString().Trim();

            return new Token(value, IsQuoted, QuoteChar);
        }

        public void Reset()
        {
            _sb.Clear();
            IsQuoted = false;
            QuoteChar = null;
        }

        public bool HasData()
        {
            return _sb.Length > 0;
        }
    }
}
