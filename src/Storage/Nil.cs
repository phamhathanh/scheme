using System;

namespace Scheme.Storage
{
    internal sealed class Nil : Atom
    {
        public static Nil Instance = new Nil();

        private Nil()
        { }

        public override Object Evaluate(Environment enviroment)
            => this;

        public override string ToString()
            => "()";
    }
}