﻿using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.CR
{
    public class CarriageReturnLexerTests
    {
        [Theory]
        [InlineData("\x0D")]
        public void ReadSuccess(string input)
        {
            var factory = new CarriageReturnLexerFactory(new TerminalLexerFactory());
            var carriageReturnLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = carriageReturnLexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
