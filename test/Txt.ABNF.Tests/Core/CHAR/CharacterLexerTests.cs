﻿using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.CHAR
{
    public class CharacterLexerTests
    {
        [Theory]
        [InlineData("\x01")]
        [InlineData("\x10")]
        [InlineData("\x20")]
        [InlineData("\x30")]
        [InlineData("\x40")]
        [InlineData("\x50")]
        [InlineData("\x60")]
        [InlineData("\x70")]
        [InlineData("\x7F")]
        public void ReadSuccess(string input)
        {
            var factory = new CharacterLexerFactory(new ValueRangeLexerFactory());
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
