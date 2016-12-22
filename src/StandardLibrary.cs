using System.Collections.Generic;
using Scheme.Storage;

namespace Scheme
{
    internal static class StandardLibrary
    {
        public static Dictionary<Identifier, Object> Procedures = new Dictionary<Identifier, Object>
        {
            [Identifier.FromString("quote")] = new Procedure(Quote)
        };

        private static Object Quote(Object args, Environment env)
            => args;
            // TODO: Validate: 1 arg only.

/*
        private static Object IsPair(Object args, Environment env)
            => args

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