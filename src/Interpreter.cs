using System.Diagnostics;
using Scheme.Storage;

namespace Scheme
{
    internal static class Interpreter
    {
        public static Object Interpret(Object datum)
        {
            var globalEnvironment = new Environment(StandardLibrary.Procedures, null);
            return Evaluate(datum, globalEnvironment);
        }

        public static Object Evaluate(Object datum, Environment environment)
        {
            if (datum is Atom)
                return datum;
            Debug.Assert(datum is ConsCell);
            return Evaluate((ConsCell)datum, environment);
        }

        private static Object Evaluate(ConsCell expression, Environment environment)
        {
            var keyword = expression.Car;
            if (keyword is ConsCell)
                keyword = Evaluate((ConsCell)keyword as ConsCell, environment);

            bool isAKeyword = keyword is Symbol;
            if (!isAKeyword)
                throw new SemanticException($"Syntax error: {keyword} is not a keyword.");

            var dereferenceResult = environment.LookUp((Symbol)keyword);
            if (dereferenceResult is Procedure)
            {
                var procedure = (Procedure)dereferenceResult;
                var args = expression.Cdr;
                // TODO: Check args form.
                bool argsIsValid = (args is ConsCell) || (args is Nil);
                if (!argsIsValid)
                    throw new SyntaxException($"Syntax error: Invalid expression.");
                return procedure.Invoke(args, environment);
            }

            throw new System.NotImplementedException();
        }
    }
}