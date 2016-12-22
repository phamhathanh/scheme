using System;

namespace Scheme.Lexicon
{
    internal sealed class Literal : Atom
    {
        public Literal(object value) : base(value)
        { }
    }
}