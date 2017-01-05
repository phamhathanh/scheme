using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Object> GetListItems()
        {
            Object current = this;
            while (true)
            {
                if (current is Nil)
                    yield break;
                if (!(current is ConsCell))
                    throw new System.InvalidOperationException("This is not a list.");
                var ccCurrent = (ConsCell)current;
                yield return ccCurrent.Car;
                current = ccCurrent.Cdr;
            }
        }

        public override sealed Object Evaluate(Environment environment)
        {
            var operatorExpression = Car;
            Object _operator = operatorExpression;
            bool canBeEvaluated = operatorExpression is ConsCell || operatorExpression is Symbol;
            if (canBeEvaluated)
                _operator = operatorExpression.Evaluate(environment);

            if (!(_operator is Procedure))
                throw new SemanticException($"Syntax error: {operatorExpression} does not evaluate to a procedure or macro.");

            var procedure = (Procedure)_operator;
            IEnumerable<Object> args;
            if (Cdr is Nil)
                args = Enumerable.Empty<Object>();
            else if (Cdr is ConsCell)
                args = GetArgs((ConsCell)Cdr);
            else
                throw new SyntaxException($"Syntax error: Invalid expression.");

            return procedure.Invoke(args, environment);
        }

        private IEnumerable<Object> GetArgs(ConsCell args)
        {
            try
            {
                return args.GetListItems();
            }
            catch (System.InvalidOperationException)
            {
                throw new SyntaxException("Syntax error: Invalid expression.");
            }
        }

        public override sealed string ToString()
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