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

        public override sealed Object Evaluate(Environment environment)
            => this;

        public override sealed string ToString()
            => Value.ToString();
    }
}