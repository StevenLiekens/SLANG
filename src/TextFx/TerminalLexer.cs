﻿namespace TextFx
{
    using System;

    /// <summary>
    ///     Provides methods for reading a terminal value. This implementation is case-sensitive. For a case-insensitive
    ///     implementation, use the <see cref="CaseInsensitiveTerminalLexer" /> class.
    /// </summary>
    public class TerminalLexer : Lexer<Terminal>
    {
        private readonly char terminal;

        public TerminalLexer(char terminal)
        {
            this.terminal = terminal;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Terminal element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner");
            }

            var context = scanner.GetContext();
            char c;
            if (!scanner.EndOfInput && scanner.TryMatch(this.terminal, out c))
            {
                element = new Terminal(c, context);
                if (previousElementOrNull != null)
                {
                    element.PreviousElement = previousElementOrNull;
                    previousElementOrNull.NextElement = element;
                }

                return true;
            }

            element = default(Terminal);
            return false;
        }
    }
}