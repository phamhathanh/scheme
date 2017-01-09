using System.Collections.Generic;
using System.Linq;
using Scheme.Storage;

namespace Scheme.Library
{
    internal static class Numbers
    {
        public static Dictionary<Symbol, Object> Procedures
            => procedures.ToDictionary(kvp => new Symbol(kvp.Key),
                            kvp => (Object)new Procedure(kvp.Value));

        private static readonly Dictionary<string, Procedure.Function> procedures =
        // TODO: Properly encapsulate.
                    new Dictionary<string, Procedure.Function>
                    {
                        ["+"] = Plus,
                        ["-"] = Minus,
                        ["*"] = Multiply
                    };
                    
        private static Object Plus(IEnumerable<Object> args, Environment env)
        {
            // TODO: Validate: number.
            var sum = args.Sum(arg => ((Number)arg.Evaluate(env)).Value);
            return new Number(sum);
        }
                    
        private static Object Minus(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            if (argsArray.Length == 0)
                throw new System.ArgumentException(
                    $"Wrong number of arguments: At least 1 expected instead of 0.");

            // TODO: Validate: number.
            if (argsArray.Length == 1)
                return new Number(-((Number)argsArray[0].Evaluate(env)).Value);
                
            var difference = argsArray.Skip(1).Aggregate(((Number)argsArray[0].Evaluate(env)).Value, (acc, arg) => acc - ((Number)arg.Evaluate(env)).Value);
            // TODO: Make these less verbose.
            return new Number(difference);
        }
                    
        private static Object Multiply(IEnumerable<Object> args, Environment env)
        {
            // TODO: Validate: number.
            var product = args.Aggregate(1.0, (acc, arg) => acc*((Number)arg.Evaluate(env)).Value);
            return new Number(product);
        }
    }
}