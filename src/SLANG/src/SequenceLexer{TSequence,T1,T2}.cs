﻿namespace SLANG
{
    using System;

    /// <summary>Provides the base class for lexers whose lexer rule has is a sequence of two elements.</summary>
    /// <typeparam name="TSequence">The type of the lexer rule.</typeparam>
    /// <typeparam name="T1">The type of the first element in the sequence.</typeparam>
    /// <typeparam name="T2">The type of the second element in the sequence.</typeparam>
    public abstract class SequenceLexer<TSequence, T1, T2> : Lexer<TSequence>
        where TSequence : Sequence<T1, T2>
        where T1 : Element
        where T2 : Element
    {
        /// <summary>Initializes a new instance of the <see cref="SequenceLexer{TSequence,T1,T2}"/> class for a specified rule.</summary>
        /// <param name="ruleName">The name of the lexer rule. Rule names are case insensitive.</param>
        /// <exception cref="ArgumentException">The value of <paramref name="ruleName"/> is a <c>null</c> reference (<c>Nothing</c> in Visual Basic) -or- the value of <paramref name="ruleName"/> does not start with a letter -or- the value of <paramref name="ruleName"/> contains one or more characters that are not letters, digits or hyphens.</exception>
        public SequenceLexer(string ruleName)
            : base(ruleName)
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out TSequence element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TSequence);
                return false;
            }

            var context = scanner.GetContext();
            T1 element1;
            if (!this.TryRead(scanner, out element1))
            {
                element = default(TSequence);
                return false;
            }

            T2 element2;
            if (!this.TryRead(scanner, out element2))
            {
                scanner.PutBack(element1.Data);
                element = default(TSequence);
                return false;
            }

            element = this.CreateInstance(element1, element2, context);
            return true;
        }

        /// <summary>Creates a new instance of the lexer rule with the given elements.</summary>
        /// <param name="element1">The first element in the sequence.</param>
        /// <param name="element2">The second element in the sequence.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TSequence CreateInstance(T1 element1, T2 element2, ITextContext context);

        /// <summary>Attempts to read the first element of the sequence. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead(ITextScanner scanner, out T1 element);

        /// <summary>Attempts to read the second element of the sequence. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead(ITextScanner scanner, out T2 element);
    }
}