using System;

namespace Scheme.Storage
{
    internal sealed class Nil : Atom
    {
        public static Nil Instance = new Nil();

        private Nil()
        { }

        public override string ToString()
            => "()";
    }
}