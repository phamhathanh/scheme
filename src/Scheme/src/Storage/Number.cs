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

        public static bool operator ==(Number number1, Number number2)
            => number1.Value == number2.Value;

        public static bool operator !=(Number number1, Number number2)
            => number1.Value != number2.Value;

        public override bool Equals (object other)
            => other is Number && (Number)other == this;
        
        public override int GetHashCode()
            => Value.GetHashCode();

        public override sealed string ToString()
            => Value.ToString();
    }
}