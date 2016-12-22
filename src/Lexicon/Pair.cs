using System;

namespace Scheme.Lexicon
{
    internal sealed class Pair : Object
    {
        public Object Car { get; private set; }
        public Object Cdr { get; private set; }

        public Pair(Object car, Object cdr)
        {
            // Check if null.
            Car = car;
            Cdr = cdr;
        }

        public override string ToString()
            => (this == Nil)? "()" : $"({Car} . {Cdr})";

        public static Pair Nil = new Pair(null, null);
    }
}