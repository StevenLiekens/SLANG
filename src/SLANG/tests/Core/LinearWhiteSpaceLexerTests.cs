﻿namespace SLANG.Core
{
    using SLANG.Core.CR;
    using SLANG.Core.CRLF;
    using SLANG.Core.HTAB;
    using SLANG.Core.LF;
    using SLANG.Core.LWSP;
    using SLANG.Core.SP;
    using SLANG.Core.WSP;

    using Xunit;

    public class LinearWhiteSpaceLexerTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\r\n\t")]
        [InlineData("\r\n   \t")]
        [InlineData("\t\r\n \t \r\n  \t ")]
        public void ReadSuccess(string input)
        {
            // General
            var terminalsLexerFactory = new TerminalsLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var repetitionLexerFactory = new RepetitionLexerFactory();

            // SP
            var spaceLexerFactory = new SpaceLexerFactory(terminalsLexerFactory);

            // HTAB
            var horizontalTabLexerFactory = new HorizontalTabLexerFactory(terminalsLexerFactory);

            // WSP
            var whiteSpaceLexerFactory = new WhiteSpaceLexerFactory(
                spaceLexerFactory,
                horizontalTabLexerFactory,
                alternativeLexerFactory);

            // CR
            var carriageReturnLexerFactory = new CarriageReturnLexerFactory(terminalsLexerFactory);

            // LF
            var lineFeedLexerFactory = new LineFeedLexerFactory(terminalsLexerFactory);

            // CRLF
            var endOfLineLexerFactory = new EndOfLineLexerFactory(
                carriageReturnLexerFactory,
                lineFeedLexerFactory,
                sequenceLexerFactory);

            // LWSP
            var linearWhiteSpaceLexerFactory = new LinearWhiteSpaceLexerFactory(
                whiteSpaceLexerFactory,
                endOfLineLexerFactory,
                sequenceLexerFactory,
                alternativeLexerFactory,
                repetitionLexerFactory);
            var linearWhiteSpaceLexer = linearWhiteSpaceLexerFactory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var linearWhiteSpace = linearWhiteSpaceLexer.Read(scanner);
                Assert.NotNull(linearWhiteSpace);
                Assert.Equal(input, linearWhiteSpace.Data);
            }
        }
    }
}