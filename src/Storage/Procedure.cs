using System;

namespace Scheme.Storage
{
    internal sealed class Procedure : Atom
    {
        public delegate Object Function(Object arguments, Environment environment);

        private readonly Function function;

        public Procedure(Function function)
        {
            this.function = function;
        }

        public Object Invoke(Object arguments, Environment environment)
            => function.Invoke(arguments, environment);

        public override Object Evaluate(Environment environment)
            => this;

        public override string ToString()
            => "Lambda.";
        // Should include id or something.
    }
}