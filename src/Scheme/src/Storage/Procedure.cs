using System;

namespace Scheme.Storage
{
    internal sealed class Procedure : Atom
    {
        public delegate Object Function(Object argumentList, Environment environment);

        private readonly Function function;

        public Procedure(Function function)
        {
            this.function = function;
        }

        public Object Invoke(Object argumentList, Environment environment)
            => function.Invoke(argumentList, environment);

        public override string ToString()
            => "#<procedure>";
        // Doesn't look good.
    }
}