using System;

namespace Scheme.Storage
{
    internal sealed class Nil : Atom
    {
        public static Nil Instance = new Nil();

        private Nil()
        { }

        public override sealed Object Evaluate(Environment environment)
        {
            throw new SyntaxException("Cannot evaluate nil.");
        }

        public override string ToString()
            => "()";
    }
}