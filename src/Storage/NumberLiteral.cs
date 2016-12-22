using System;

namespace Scheme.Storage
{
    internal sealed class NumberLiteral : Atom
    {
        private readonly double value;
        
        public NumberLiteral(double value)
        {
            this.value = value;
        }

        public override Object Evaluate(Environment enviroment)
            => this;

        public override string ToString()
            => value.ToString();
    }
}