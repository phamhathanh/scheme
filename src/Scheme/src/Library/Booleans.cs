using System.Collections.Generic;
using System.Linq;
using Scheme.Storage;

namespace Scheme.Library
{
    internal static class Booleans
    {
        public static Dictionary<Symbol, Object> Procedures
            => procedures.ToDictionary(kvp => new Symbol(kvp.Key),
                            kvp => (Object)new Procedure(kvp.Value));

        private static readonly Dictionary<string, Procedure.Function> procedures =
                    new Dictionary<string, Procedure.Function>
                    {
                        ["not"] = Not,
                        ["boolean?"] = IsBoolean
                    };

        private static Object Not(IEnumerable<Object> args)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            bool isFalse = argsArray[0] == Boolean.FALSE;
            return Boolean.FromBool(isFalse);
        }
        
        private static void ValidateArgCount(int expected, int actual)
        {
            if (actual == expected)
                return;
            var message = $"Wrong number of arguments: {expected} expected instead of {actual}.";
            // TODO: include procedure name.
            throw new SyntaxException(message);
        }

        private static Object IsBoolean(IEnumerable<Object> args)
        {
            var argsArray = args.ToArray();
            ValidateArgCount(1, argsArray.Length);

            bool isBoolean = argsArray[0] is Boolean;
            return Boolean.FromBool(isBoolean);
        }
    }
}