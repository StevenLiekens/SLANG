﻿namespace Text.Scanning
{
    public abstract class Lexer<TToken> : ILexer<TToken>
        where TToken : Token
    {
        /// <inheritdoc />
        public virtual void PutBack(ITextScanner scanner, TToken token)
        {
            var data = token.Data;
            for (var i = data.Length - 1; i >= 0; i--)
            {
                scanner.PutBack(data[i]);
            }
        }

        /// <inheritdoc />
        public abstract TToken Read(ITextScanner scanner);

        /// <inheritdoc />
        public abstract bool TryRead(ITextScanner scanner, out TToken token);

        /// <summary>Utility method. Sets a specified token to its default value, and returns <c>false</c>.</summary>
        protected static bool Default(out TToken token)
        {
            token = default(TToken);
            return false;
        }
    }
}