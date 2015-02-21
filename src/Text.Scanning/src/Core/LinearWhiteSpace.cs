﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents the LWSP rule: any linear white space. The LWSP rule permits lines containing only white space.</summary>
    public class LinearWhiteSpace : Element
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<Element> lwsp;

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.LinearWhiteSpace"/> class with the specified
        /// characters and context.</summary>
        /// <param name="data">The collection of 'LWSP' components.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public LinearWhiteSpace(IList<CrLfWSpPair> data, ITextContext context)
            : this(Linearize(data), context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
        }

        /// <summary>TODO </summary>
        /// <param name="data">TODO </param>
        /// <param name="context">TODO </param>
        private LinearWhiteSpace(IList<Element> data, ITextContext context)
            : base(string.Concat(data) ?? string.Empty, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.lwsp = data;
        }

        /// <summary>Gets the collection of 'LWSP' components.</summary>
        public IList<Element> LWsp
        {
            get
            {
                return this.lwsp;
            }
        }

        /// <summary>TODO </summary>
        /// <param name="data">TODO </param>
        /// <returns></returns>
        [Pure]
        private static IList<Element> Linearize(IList<CrLfWSpPair> data)
        {
            Contract.Requires(data != null);
            Contract.Ensures(Contract.Result<IList<Element>>() != null);
            var elements = new List<Element>();
            foreach (var pair in data)
            {
                if (pair == null)
                {
                    continue;
                }

                var crLf = pair.CrLf;
                if (crLf != null)
                {
                    elements.Add(crLf);
                }

                elements.Add(pair.Wsp);
            }

            return elements;
        }

        /// <summary>TODO </summary>
        public class CrLfWSpPair
        {
            /// <summary>TODO </summary>
            private readonly EndOfLine crLf;

            /// <summary>TODO </summary>
            private readonly WhiteSpace wsp;

            /// <summary>TODO </summary>
            /// <param name="wsp">TODO </param>
            public CrLfWSpPair(WhiteSpace wsp)
            {
                Contract.Requires(wsp != null);
                this.wsp = wsp;
            }

            /// <summary>TODO </summary>
            /// <param name="crLf">TODO </param>
            /// <param name="wsp">TODO </param>
            public CrLfWSpPair(EndOfLine crLf, WhiteSpace wsp)
            {
                Contract.Requires(crLf != null);
                Contract.Requires(wsp != null);
                Contract.Requires(wsp.Offset == crLf.Offset + 2);
                this.crLf = crLf;
                this.wsp = wsp;
            }

            /// <summary>TODO </summary>
            public EndOfLine CrLf
            {
                get
                {
                    return this.crLf;
                }
            }

            /// <summary>TODO </summary>
            public WhiteSpace Wsp
            {
                get
                {
                    Contract.Ensures(Contract.Result<WhiteSpace>() != null);
                    return this.wsp;
                }
            }

            /// <summary>TODO </summary>
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.wsp != null);
                Contract.Invariant(this.crLf == null || this.wsp.Offset == this.crLf.Offset + 2);
            }
        }
    }
}