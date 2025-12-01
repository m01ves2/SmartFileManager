using SmartFileManager.App.Interfaces;
using SmartFileManager.Core.Models;
using System.ComponentModel.Design;
using System.Text;

namespace SmartFileManager.App.Services
{
    public class Tokenizer : ITokenizer
    {
        enum State { Outside, Inside, InsideQuoted, Escape };
        public IReadOnlyList<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();
            TokenBuilder builder = new TokenBuilder();
            State state = State.Outside;
            State previousState = State.Outside;

            for (int i = 0; i < input.Length; i++) {
                char c = input[i];
                
                if(state != State.Escape)
                    previousState = state;

                switch (state) {
                    case State.Outside:
                        state = ProcessCharOutsideToken(c, builder);
                        break;

                    case State.Inside:
                        Token? token = null;
                        state = ProcessCharInsideToken(c, builder, out token);
                        if (token != null) {
                            tokens.Add(token);
                        }
                        break;

                    case State.InsideQuoted:
                        Token? tokenInsideQuated = null;
                        state = ProcessCharInsideQuotedToken(c, builder, out tokenInsideQuated);
                        if (tokenInsideQuated != null) {
                            tokens.Add(tokenInsideQuated);
                        }
                        break;

                    case State.Escape:
                        state = ProcessEscape(c, builder, previousState);
                        break;
                }

            }

            if (builder.HasData())
                tokens.Add(builder.Build());

            return tokens;
        }

        private State ProcessCharOutsideToken(char c, TokenBuilder builder)
        {
            if (builder.HasData() || builder.IsQuoted || builder.QuoteChar != null)
                throw new InvalidOperationException("Tokenizer internal error: Outside but builder not empty");

            if (c == ' ') {
                return State.Outside;
            }
            else if (c == '\"' || c == '\'') {
                builder.Reset(); //just in case
                builder.QuoteChar = c;
                builder.IsQuoted = true;
                return State.InsideQuoted;
            }
            else if (c == '\\') {
                throw new InvalidOperationException("Incorrect input: unexpected escape-symbol");
            }
            else {
                builder.Append(c);
                return State.Inside;
            }
        }

        private State ProcessCharInsideToken(char c, TokenBuilder builder, out Token? newToken)
        {
            if (builder.IsQuoted || builder.QuoteChar != null)
                throw new InvalidOperationException("Tokenizer internal error: InsideToken but quoted flags set");

            newToken = null;
            if (c == ' ') {
                newToken = builder.Build(); //close token
                builder.Reset();
                return State.Outside;
            }
            else if (c == '\"' | c == '\'') {
                throw new InvalidOperationException("Incorrect input: unexpected quote");
            }
            else if (c == '\\') {
                return State.Escape;
            }
            else {
                builder.Append(c);
                return State.Inside;
            }
        }

        private State ProcessCharInsideQuotedToken(char c, TokenBuilder builder, out Token? newToken)
        {
            if (!builder.IsQuoted || builder.QuoteChar == null)
                throw new InvalidOperationException("Tokenizer internal error: InsideQuoted but QuoteChar not set");

            newToken = null;
            if ((c == '\"' || c == '\'') && c == builder.QuoteChar) {
                newToken = builder.Build();
                builder.Reset();
                return State.Outside;
            }
            else if (c.Equals('\\')) {
                return State.Escape;
            }
            else {
                builder.Append(c);
                return State.InsideQuoted;
            }
        }

        private State ProcessEscape(char c, TokenBuilder builder, State previousState)
        {
            // Escape-логика работает ТОЛЬКО внутри кавычек.
            // Внутри кавычек поддерживаем \" \' \\.
            // Всё остальное пишем как есть: \ + символ.

            if (previousState == State.InsideQuoted) {
                if (c == builder.QuoteChar) {
                    // \" или \' → добавляем кавычку
                    builder.Append(c);
                }
                else if (c == '\\') {
                    // \\ → добавляем один \
                    builder.Append('\\');
                }
                else {
                    // \x → добавляем '\' и 'x'
                    builder.Append('\\');
                    builder.Append(c);
                }

                return State.InsideQuoted;
            }
            else
                // ВНЕ quoted токенов escape — ошибка
                throw new InvalidOperationException("Unexpected escape outside quoted string");
        }
    }
}
