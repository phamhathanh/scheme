using System;

namespace Scheme.Storage
{
    internal sealed class ConsCell : Object
    {
        public static ConsCell Nil = new ConsCell(null, null);
        
        public Object Car { get; private set; }
        public Object Cdr { get; private set; }

        public ConsCell(Object car, Object cdr)
        {
            // Check if null.
            Car = car;
            Cdr = cdr;
        }

        public override string ToString()
            => (this == Nil)? "()" : $"({Car} . {Cdr})";
    }
}