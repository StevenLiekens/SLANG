﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the BIT rule: 1 bit. Unicode: U+0030 - U+0031.</summary>
    public class Bit : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.Bit"/> class with a specified character
        /// and context.</summary>
        /// <param name="data">The bit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Bit(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == '\u0030' || data == '\u0031');
            Contract.Requires(context != null);
        }
    }
}