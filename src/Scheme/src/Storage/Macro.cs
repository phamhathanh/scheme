using System.Collections.Generic;

namespace Scheme.Storage
{
    internal sealed class Macro : Atom
    {
        public delegate Object Transformation(Object datum, Environment environment);

        private readonly Transformation transformation;

        public Macro(Transformation transformation)
        {
            this.transformation = transformation;
        }

        public Object Expand(Object datum, Environment environment)
            => transformation.Invoke(datum, environment);

        public override sealed Object Evaluate(Environment environment)
            => this;
            // Not sure if this can be evaluated.

        public override string ToString()
            => "#<macro>";
        // TODO: Change representation.
    }
}