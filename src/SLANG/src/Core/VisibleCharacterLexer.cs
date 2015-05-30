﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibleCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public class VisibleCharacterLexer : AlternativeLexer<VisibleCharacter>
    {
        /// <summary>Initializes a new instance of the <see cref="VisibleCharacterLexer"/> class.</summary>
        public VisibleCharacterLexer()
            : base("VCHAR", '\x21', '\x7E')
        {
        }
    }
}