using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Scheme.Storage;

namespace Scheme
{
    internal static class Evaluator
    {
        public static Object Evaluate(Object datum)
        {
            var globalEnvironment = new Environment(StandardLibrary.Procedures, null);
            return Evaluate(datum, globalEnvironment);
        }

        public static Object Evaluate(Object datum, Environment environment)
        {
            if (datum is Atom)
            {
                if (datum is Symbol)
                    return Evaluate((Symbol)datum, environment);
                return datum;
            }
            Debug.Assert(datum is ConsCell);
            return Evaluate((ConsCell)datum, environment);
        }

        private static Object Evaluate(ConsCell expression, Environment environment)
        {
            var operatorExpression = expression.Car;
            Object _operator;
            if (operatorExpression is ConsCell)
                _operator = Evaluate((ConsCell)operatorExpression, environment);
            else if (operatorExpression is Symbol)
                _operator = Evaluate((Symbol)operatorExpression, environment);
            else
                throw new SemanticException($"Syntax error: {operatorExpression} does not evaluate to a procedure or macro.");

            if (_operator is Procedure)
            {
                var procedure = (Procedure)_operator;
                IEnumerable<Object> args;
                if (expression.Cdr is Nil)
                    args = Enumerable.Empty<Object>();
                else if (expression.Cdr is ConsCell)
                    args = GetArgs((ConsCell)expression.Cdr);
                else
                    throw new SyntaxException($"Syntax error: Invalid expression.");

                return procedure.Invoke(args, environment);
            }

            throw new System.NotImplementedException();
        }

        private static IEnumerable<Object> GetArgs(ConsCell args)
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

        private static Object Evaluate(Symbol symbol, Environment environment)
            => environment.LookUp(symbol);
    }
}