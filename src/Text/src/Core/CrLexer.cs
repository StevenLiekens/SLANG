﻿namespace Text.Core
{
    public class CrLexer : Lexer<CrToken>
    {
        public CrLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override CrToken Read()
        {
            var context = this.Scanner.GetContext();
            CrToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected 'CR'", context);
        }

        public override bool TryRead(out CrToken token)
        {
            var context = this.Scanner.GetContext();
            if (this.Scanner.TryMatch('\r'))
            {
                token = new CrToken(context);
                return true;
            }

            token = default(CrToken);
            return false;
        }
    }
}
