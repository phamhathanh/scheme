using System;

namespace Scheme.Storage
{
    internal sealed class StringLiteral : Atom
    {
        private readonly string value;
        
        public StringLiteral(string value)
        {
            this.value = value;
        }

        public override Object Evaluate(Environment environment)
            => this;

        public override string ToString()
            => $"\"{value}\"";
    }
}