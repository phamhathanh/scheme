using System.Collections.Generic;

namespace Scheme.Storage
{
    internal sealed class Procedure : Atom
    {
        public delegate Object Function(IEnumerable<Object> arguments);

        private readonly Function function;

        public Procedure(Function function)
        {
            this.function = function;
        }

        public Object Apply(IEnumerable<Object> arguments)
            => function.Invoke(arguments);

        public override sealed Object Evaluate(Environment environment)
            => this;
            // Not sure if this can be evaluated.

        public override string ToString()
            => "#<procedure>";
        // TODO: Change representation.
    }
}