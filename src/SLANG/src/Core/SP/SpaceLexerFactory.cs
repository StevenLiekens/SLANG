﻿namespace SLANG.Core.SP
{
    using System;

    public class SpaceLexerFactory : ILexerFactory<Space>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public SpaceLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<Space> Create()
        {
            var spaceTerminalLexer = this.terminalLexerFactory.Create('\x20');
            return new SpaceLexer(spaceTerminalLexer);
        }
    }
}