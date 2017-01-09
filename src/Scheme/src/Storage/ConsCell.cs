using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheme.Storage
{
    internal sealed class ConsCell : Object
    {
        public static readonly ConsCell Nil = new ConsCell();

        public Object Car { get; private set; }
        public Object Cdr { get; private set; }

        private ConsCell() { }
        // For Nil.

        public ConsCell(Object car, Object cdr)
        {
            if (car == null || cdr == null)
                throw new System.ArgumentException("Object is null.");

            Car = car;
            Cdr = cdr;
        }

        public override Object Evaluate(Environment environment)
        {
            if (this == Nil)
                throw new SemanticException("Cannot evaluate nil.");

            var operatorExpression = Car;
            Object _operator = operatorExpression;
            bool canBeEvaluated = operatorExpression is ConsCell || operatorExpression is Symbol;
            if (canBeEvaluated)
                _operator = operatorExpression.Evaluate(environment);
                // TODO: Is there a case where a procedure object is directly assigned to Car?

            if (!(Cdr is ConsCell))
                throw new SyntaxException("Syntax error: Invalid expression.");
            var subexpressions = GetSubexpressions((ConsCell)Cdr);

            if (_operator is Procedure)
            {
                var procedure = (Procedure)_operator;
                var args = from subexpression in subexpressions
                           select subexpression.Evaluate(environment);
                return procedure.Apply(args);
            }

            if (_operator is Macro)
            {
                var macro = (Macro)_operator;
                return macro.Expand(subexpressions, environment);
            }

            throw new SemanticException($"Syntax error: {operatorExpression} does not evaluate to a procedure or macro.");
        }

        private IEnumerable<Object> GetSubexpressions(ConsCell args)
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

        public IEnumerable<Object> GetListItems()
        {
            ConsCell current = this;
            while (true)
            {
                if (current == Nil)
                    yield break;
                yield return current.Car;
                if (!(current.Cdr is ConsCell))
                    throw new System.InvalidOperationException("This is not a list.");
                current = (ConsCell)current.Cdr;
            }
        }

        public bool CheckIfIsList()
        {
            if (this == Nil)
                return true;
            if (!(Cdr is ConsCell))
                return false;
            return ((ConsCell)Cdr).CheckIfIsList();
            // TODO: Circular list case.
        }

        public override sealed string ToString()
        {
            if (this == Nil)
                return "()";
            var output = new StringBuilder($"({Car}");
            Object current = Cdr;
            while (current is ConsCell)
            {
                if (current == Nil)
                    return output.Append(')').ToString();

                var cc = (ConsCell)current;
                output.Append($" {cc.Car}");
                current = cc.Cdr;
            }

            return output.Append($" . {current})").ToString();
            // TODO: Circular list case.
        }
    }
}