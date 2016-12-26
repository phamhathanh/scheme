using System;

namespace Scheme.Storage
{
    public sealed class Number : Atom
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