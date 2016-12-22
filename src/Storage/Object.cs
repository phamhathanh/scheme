using System;

namespace Scheme.Storage
{
    internal abstract class Object
    {
        public abstract Object Evaluate(Environment environment);
        public abstract override string ToString();
    }
}