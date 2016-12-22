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

        private static Object Quote(Object args, Environment env)
        {
            bool noArg = args is Nil;
            if (noArg)
                throw new System.ArgumentException("Wrong number of argument: 1 expected instead of 0.");
            if (!(args is ConsCell))
                throw new SyntaxException("Wrong syntax for function invocation.");
            var ccArgs = (ConsCell)args;
            bool oneArg = ccArgs.Cdr is Nil;
            if (!oneArg)
                throw new System.ArgumentException("Wrong number of argument: 1 expected.");
            return ccArgs.Car;
        }



        private static Object IsPair(Object args, Environment env)
        {
            var ccArgs = (ConsCell)args;
            bool onlyOneArg = ccArgs.Cdr is Nil;
            if (!onlyOneArg)
                throw new System.ArgumentException("Wrong number of argument: 1 expected.");
                // TODO: Add number of args and procedure name in debug information.

            var result = Interpreter.Evaluate(ccArgs.Car, env);
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