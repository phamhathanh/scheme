using System.Collections.Generic;
using System.Linq;
using Scheme.Storage;

namespace Scheme
{
    internal static class StandardLibrary
    {
        public static Dictionary<Symbol, Object> Procedures
            => procedures.ToDictionary(kvp => Symbol.FromString(kvp.Key),
                                    kvp => (Object)new Procedure(kvp.Value));

        private static Dictionary<string, Procedure.Function> procedures =
                    new Dictionary<string, Procedure.Function>
                    {
                        ["quote"] = Quote,
                        ["pair?"] = IsPair,
                        ["+"] = Plus,
                        ["lambda"] = Lambda
                    };

        private static Object Quote(Object argList, Environment env)
        {
            var args = GetArgs(argList).ToArray();
            ValidateArgCount(1, args.Length);

            return args[0];
        }

        private static IEnumerable<Object> GetArgs(Object args)
        {
            try
            {
                return GetListItems(args);
            }
            catch (System.ArgumentException)
            {
                throw new SyntaxException("Wrong syntax for procedure call.");
            }
        }

        private static IEnumerable<Object> GetListItems(Object head)
        {
            var current = head;
            while (true)
            {
                if (current is Nil)
                    yield break;
                if (!(current is ConsCell))
                    throw new System.ArgumentException("Not a list.");
                var ccCurrent = (ConsCell)current;
                yield return ccCurrent.Car;
                current = ccCurrent.Cdr;
            }
        }

        private static void ValidateArgCount(int expected, int actual)
        {
            if (actual == expected)
                return;
            var message = $"Wrong number of arguments: {expected} expected instead of {actual}.";
            throw new System.ArgumentException(message);
        }

        private static Object IsPair(Object argList, Environment env)
        {
            var args = GetArgs(argList).ToArray();
            ValidateArgCount(1, args.Length);

            var result = Interpreter.Evaluate(args[0], env);
            bool isPair = result is ConsCell;
            return Boolean.FromBool(isPair);
        }

        private static Object Plus(Object argList, Environment env)
        {
            // TODO: Validate: number.
            var args = GetArgs(argList);
            var result = args.Sum(arg => ((Number)Interpreter.Evaluate(arg, env)).Value);
            return new Number(result);
        }

        private static Object Lambda(Object argList, Environment env)
        {
            var args = GetArgs(argList).ToArray();
            if (args.Length < 2)
                throw new System.ArgumentException(
                    $"Wrong number of arguments: At least 2 expected instead of {args.Length}.");

            var formals = args.First();
            var body = args.Skip(1);

            // Assuming formal list only.
            // TODO: consider formals forms.
            // TODO: validate if items are identifiers.
            var symbols = from item in GetListItems(formals)
                          select (Symbol)item;

            return new Procedure((_argList, _env) =>
            {
                // TODO: Validate many things...
                var _args = GetListItems(_argList);
                var bindings = symbols.Zip(_args, (s, a) => new { Key = s, Value = a })
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                var lambdaEnv = new Environment(bindings, _env);
                Object result = null;
                foreach (var expression in body)
                    result = Interpreter.Evaluate(expression, lambdaEnv);
                return result;
            });
        }
    }
}