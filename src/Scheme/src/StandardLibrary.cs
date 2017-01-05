using System.Collections.Generic;
using System.Linq;
using Scheme.Storage;

namespace Scheme
{
    internal static class StandardLibrary
    {
        public static Dictionary<Symbol, Object> Procedures
            => procedures.ToDictionary(kvp => new Symbol(kvp.Key),
                                    kvp => (Object)new Procedure(kvp.Value));

        private static Dictionary<string, Procedure.Function> procedures =
                    new Dictionary<string, Procedure.Function>
                    {
                        ["quote"] = Quote,
                        ["pair?"] = IsPair,
                        ["cons"] = Cons,
                        ["car"] = Car,
                        ["cdr"] = Cdr,
                        ["null?"] = IsNull,
                        ["list?"] = IsList,
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

        private static Object IsPair(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            var result = argsArray[0].Evaluate(env);
            bool isPair = result is ConsCell;
            return Boolean.FromBool(isPair);
        }

        private static Object Cons(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(2, argsArray.Length);

            var car = argsArray[0].Evaluate(env);
            var cdr = argsArray[1].Evaluate(env);
            return new ConsCell(car, cdr);
        }

        private static Object Car(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            var arg = argsArray[0].Evaluate(env);
            return GetPair(arg).Car;
        }

        private static ConsCell GetPair(Object arg)
        {
            if (!(arg is ConsCell))
                throw new SemanticException("Argument is not a pair.");
            if (arg == ConsCell.Nil)
                throw new SemanticException("Argument is Nil.");
            return (ConsCell)arg;
        }

        private static Object Cdr(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            var arg = argsArray[0].Evaluate(env);
            return GetPair(arg).Cdr;
        }

        private static Object IsNull(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            var arg = argsArray[0].Evaluate(env);
            return Boolean.FromBool(arg == ConsCell.Nil);
        }

        private static Object IsList(IEnumerable<Object> args, Environment env)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            var arg = argsArray[0].Evaluate(env);
            if (!(arg is ConsCell))
                return Boolean.FALSE;
            return Boolean.FromBool(((ConsCell)arg).CheckIfIsList());
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