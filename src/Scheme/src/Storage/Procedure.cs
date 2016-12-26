using System.Collections.Generic;

namespace Scheme.Storage
{
    internal sealed class Procedure : Atom
    {
        public delegate Object Function(IEnumerable<Object> arguments, Environment environment);

        private readonly Function function;

        public Procedure(Function function)
        {
            this.function = function;
        }

        public Object Invoke(IEnumerable<Object> arguments, Environment environment)
            => function.Invoke(arguments, environment);

        public override string ToString()
            => "#<procedure>";
        // Doesn't look good.
    }
}