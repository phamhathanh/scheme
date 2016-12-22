using System;

namespace Scheme.Storage
{
    internal sealed class Number : Atom
    {
        private readonly double value;
        
        public Number(double value)
        {
            this.value = value;
        }

        public override Object Evaluate(Environment enviroment)
            => this;

        public override string ToString()
            => value.ToString();
    }
}