using System.Collections.Generic;

namespace Scheme.Storage
{
    internal sealed class Macro : Atom
    {
        public delegate Object Transformation(IEnumerable<Object> data, Environment environment);
            // TODO: Generalize the transformation.

        private readonly Transformation transformation;

        public Macro(Transformation transformation)
        {
            this.transformation = transformation;
        }

        public Object Expand(IEnumerable<Object> data, Environment environment)
            => transformation.Invoke(data, environment);

        public override sealed Object Evaluate(Environment environment)
            => this;
            // Not sure if this can be evaluated.

        public override string ToString()
            => "#<macro>";
        // TODO: Change representation.
    }
}