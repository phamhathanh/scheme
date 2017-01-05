using System.Collections.Generic;
using System.Linq;
using Scheme.Storage;

namespace Scheme
{
    internal static class StandardLibrary
    {
        public static Dictionary<Symbol, Object> Procedures
            => procedures.Union(PairAndList.procedures)
                .ToDictionary(kvp => new Symbol(kvp.Key),
                            kvp => (Object)new Procedure(kvp.Value));

        private static Dictionary<string, Procedure.Function> procedures =
                    new Dictionary<string, Procedure.Function>
                    {
                        ["quote"] = Quote,
                        ["define"] = Define,
                        ["+"] = Plus,
                        ["lambda"] = Lambda
                    };

        private static Object Quote(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            return argsArray[0];
        }

        private static void ValidateArgCount(int expected, int actual)
        {
            if (actual == expected)
                return;
            var message = $"Wrong number of arguments: {expected} expected instead of {actual}.";
            throw new System.ArgumentException(message);
        }

        private static Object Define(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(2, argsArray.Length);
            // TODO: Other forms.
            // TODO: Ensure this being called at the top only.

            var variable = argsArray[0];
            if (!(variable is Symbol))
                throw new SyntaxException("Can only define variable.");

            var value = argsArray[1].Evaluate(env);
            env.SetBinding((Symbol)variable, value);
            return null;
        }

        private static Object Plus(IEnumerable<Object> args, Environment env)
        {
            // TODO: Validate: number.
            var result = args.Sum(arg => ((Number)arg.Evaluate(env)).Value);
            return new Number(result);
        }

        private static Object Lambda(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            if (argsArray.Length < 2)
                throw new System.ArgumentException(
                    $"Wrong number of arguments: At least 2 expected instead of {argsArray.Length}.");

            var formals = args.First();
            var body = args.Skip(1);

            // Assuming formal list only.
            // TODO: consider other forms.
            // TODO: validate if items are identifiers.
            var symbols = from item in ((ConsCell)formals).GetListItems()
                          select (Symbol)item;

            return new Procedure((_args, _env) =>
            {
                // TODO: Validate many things...
                var bindings = symbols.Zip(_args, (s, a) => new { Key = s, Value = a })
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                var lambdaEnv = new Environment(bindings, _env);
                Object result = null;
                foreach (var expression in body)
                    result = expression.Evaluate(lambdaEnv);
                return result;
            });
        }
    }
}