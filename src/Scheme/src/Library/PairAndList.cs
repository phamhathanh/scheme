using System.Collections.Generic;
using System.Linq;
using Scheme.Storage;

namespace Scheme.Library
{
    internal static class PairAndList
    {
        public static Dictionary<Symbol, Object> Procedures
            => procedures.ToDictionary(kvp => new Symbol(kvp.Key),
                            kvp => (Object)new Procedure(kvp.Value));

        private static readonly Dictionary<string, Procedure.Function> procedures =
                    new Dictionary<string, Procedure.Function>
                    {
                        ["pair?"] = IsPair,
                        ["cons"] = Cons,
                        ["car"] = Car,
                        ["cdr"] = Cdr,
                        ["null?"] = IsNull,
                        ["list?"] = IsList
                    };

        private static Object IsPair(IEnumerable<Object> args)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            bool isPair = argsArray[0] is ConsCell;
            return Boolean.FromBool(isPair);
        }

        private static void ValidateArgCount(int expected, int actual)
        {
            if (actual == expected)
                return;
            var message = $"Wrong number of arguments: {expected} expected instead of {actual}.";
            // TODO: include procedure name.
            throw new SyntaxException(message);
        }

        private static Object Cons(IEnumerable<Object> args)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(2, argsArray.Length);

            var car = argsArray[0];
            var cdr = argsArray[1];
            return new ConsCell(car, cdr);
        }

        private static Object Car(IEnumerable<Object> args)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            var arg = argsArray[0];
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

        private static Object Cdr(IEnumerable<Object> args)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            return GetPair(argsArray[0]).Cdr;
        }

        private static Object IsNull(IEnumerable<Object> args)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            return Boolean.FromBool(argsArray[0] == ConsCell.Nil);
        }

        private static Object IsList(IEnumerable<Object> args)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            var arg = argsArray[0];
            if (!(arg is ConsCell))
                return Boolean.FALSE;
            return Boolean.FromBool(((ConsCell)arg).CheckIfIsList());
        }
    }
}