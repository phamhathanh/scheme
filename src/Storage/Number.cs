using System;

namespace Scheme.Storage
{
    internal sealed class Number : Atom
    {
        public double Value { get; }

        public Number(double value)
        {
            Value = value;
        }

        public override string ToString()
            => Value.ToString();
    }
}