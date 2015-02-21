﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HTabLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The h tab lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>The h tab lexer.</summary>
    public class HTabLexer : Lexer<HTabToken>
    {
        /// <inheritdoc />
        public override HTabToken Read(ITextScanner scanner)
        {
            HTabToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'HTAB'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HTabToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(HTabToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\t'))
            {
                token = new HTabToken(context);
                return true;
            }

            token = default(HTabToken);
            return false;
        }
    }
}