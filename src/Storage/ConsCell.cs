using System;

namespace Scheme.Storage
{
    internal sealed class ConsCell : Object
    {
        public Object Car { get; private set; }
        public Object Cdr { get; private set; }

        public ConsCell(Object car, Object cdr)
        {
            // Check if null.
            Car = car;
            Cdr = cdr;
        }

        public override string ToString()
            => $"({Car} . {Cdr})";
    }
}