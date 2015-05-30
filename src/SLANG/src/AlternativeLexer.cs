﻿namespace SLANG
{
    using System;
    using System.Diagnostics;

    /// <summary>Provides the base class for lexers whose lexer rule has a range of alternatives.</summary>
    /// <typeparam name="TAlternative">The type of the lexer rule.</typeparam>
    public abstract class AlternativeLexer<TAlternative> : Lexer<TAlternative>
        where TAlternative : Alternative
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly char lowerBound;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly char upperBound;

        /// <summary>Initializes a new instance of the <see cref="AlternativeLexer{TAlternative}"/> class for an unnamed element.</summary>
        /// <param name="lowerBound">The lower bound of the range of alternatives.</param>
        /// <param name="upperBound">The upper bound of the range of alternatives.</param>
        protected AlternativeLexer(char lowerBound, char upperBound)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        /// <summary>Initializes a new instance of the <see cref="AlternativeLexer{TAlternative}"/> class for a specified rule.</summary>
        /// <param name="ruleName">The name of the lexer rule. Rule names are case insensitive.</param>
        /// <param name="lowerBound">The lower bound of the range of alternatives.</param>
        /// <param name="upperBound">The upper bound of the range of alternatives.</param>
        protected AlternativeLexer(string ruleName, char lowerBound, char upperBound)
            : base(ruleName)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out TAlternative element)
        {
            for (char c = this.lowerBound; c <= this.upperBound; c++)
            {
                Element terminal;
                if (TryReadTerminal(scanner, c, out terminal))
                {
                    element = this.CreateInstance(terminal);
                    return true;
                }
            }

            element = default(TAlternative);
            return false;
        }

        /// <summary>Creates a new instance of the lexer rule for a given alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected virtual TAlternative CreateInstance(Element element)
        {
            return (TAlternative)Activator.CreateInstance(typeof(TAlternative), element);
        }
    }
}
