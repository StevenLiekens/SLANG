﻿namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>Creates instances of the <see cref="BitLexer" /> class.</summary>
    public class BitLexerFactory : ILexerFactory<Bit>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public BitLexerFactory(IAlternativeLexerFactory alternativeLexerFactory, ITerminalLexerFactory terminalLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Bit> Create()
        {
            var bit0TerminalLexer = this.terminalLexerFactory.Create("0", StringComparer.Ordinal);
            var bit1TerminalLexer = this.terminalLexerFactory.Create("1", StringComparer.Ordinal);
            var bitAlternativeLexer = this.alternativeLexerFactory.Create(bit0TerminalLexer, bit1TerminalLexer);
            return new BitLexer(bitAlternativeLexer);
        }
    }
}