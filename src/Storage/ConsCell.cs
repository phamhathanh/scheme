using System;
using System.Text;

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
        {
            var output = new StringBuilder($"({Car}");
            var current = Cdr;
            while (current is ConsCell)
            {
                var cc = (ConsCell)current;
                output.Append($" {cc.Car}");
                current = cc.Cdr;
            }
            if (current is Nil)
                output.Append(')');
            else
                output.Append($" . {current})");
            return output.ToString();
            // TODO: Circular list case.
        }
    }
}