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
            ["pair?"] = IsPair
        };

        private static Object Quote(Object argList, Environment env)
        {
            var args = GetArgs(argList).ToArray();
            if (args.Length != 1)
                throw new System.ArgumentException($"Wrong number of arguments: 1 expected instead of {args.Length}.");
                
            return args[0];
        }

        private static IEnumerable<Object> GetArgs(Object args)
        {
            if (args is Nil)
                yield break;
            if (!(args is ConsCell))
                throw new SyntaxException("Wrong syntax for procedure call.");
            var ccArgs = (ConsCell)args;
            yield return ccArgs.Car;
            foreach (var arg in GetArgs(ccArgs.Cdr))
                yield return arg;
        }

        private static Object IsPair(Object argList, Environment env)
        {
            var args = GetArgs(argList).ToArray();
            if (args.Length != 1)
                throw new System.ArgumentException($"Wrong number of arguments: 1 expected instead of {args.Length}.");

            var result = Interpreter.Evaluate(args[0], env);
            bool isPair = result is ConsCell;
            return Boolean.FromBool(isPair);
        }

/*
        private static Object Lambda(Object args)
        {
            // TODO: Validate.
            var ccArgs = (ConsCell)args;
            var formals = ccArgs.Car;

            // Assuming formals is list.
            var bindings = formals
            var lambdaScope = new Environment()

            var body = ccArgs.Cdr;
            
            return new Procedure((a, e) => {

            });
        }
            */
    }
}