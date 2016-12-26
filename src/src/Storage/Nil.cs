using System;

namespace Scheme.Storage
{
    public sealed class Nil : Atom
    {
        public static Nil Instance = new Nil();

        private Nil()
        { }

        public override string ToString()
            => "()";
    }
}