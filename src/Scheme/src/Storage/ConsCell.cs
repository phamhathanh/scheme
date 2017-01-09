using System.Collections.Generic;
using System.Text;

namespace Scheme.Storage
{
    internal sealed class ConsCell : Object
    {
        public static readonly ConsCell Nil = new ConsCell(null, null);

        public Object Car { get; private set; }
        public Object Cdr { get; private set; }

        public ConsCell(Object car, Object cdr)
        {
            // Check if null.
            Car = car;
            Cdr = cdr;
        }

        public override sealed Object Evaluate(Environment environment)
        {
            if (this == Nil)
                throw new SyntaxException("Cannot evaluate nil.");

            var operatorExpression = Car;
            Object _operator = operatorExpression;
            bool canBeEvaluated = operatorExpression is ConsCell || operatorExpression is Symbol;
            if (canBeEvaluated)
                _operator = operatorExpression.Evaluate(environment);

            if (_operator is Procedure)
            {
                var procedure = (Procedure)_operator;

                if (!(Cdr is ConsCell))
                    throw new SyntaxException($"Syntax error: Invalid expression.");

                var args = GetArgs((ConsCell)Cdr);
                return procedure.Apply(args, environment);
            }

            if (_operator is Macro)
            {
                var macro = (Macro)_operator;
                return macro.Expand(Cdr, environment);
            }

            throw new SemanticException($"Syntax error: {operatorExpression} does not evaluate to a procedure or macro.");
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