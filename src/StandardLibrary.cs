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

        private static Object Quote(Object arguments, Environment environment)
            => arguments;
    }
}