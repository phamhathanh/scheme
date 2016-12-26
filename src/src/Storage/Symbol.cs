using System.Collections.Generic;

namespace Scheme.Storage
{
    public sealed class Symbol : Atom
    {
        private static Dictionary<string, Symbol> pool = new Dictionary<string, Symbol>();

        private readonly string value;

        private Symbol(string value)
        {
            this.value = value;
        }

        public static Symbol FromString(string input)
        {
            // TODO: Validate.
            bool exists = pool.ContainsKey(input);
            if (!exists)
                pool.Add(input, new Symbol(input));
            return pool[input];
        }

        public override string ToString()
            => value;
    }
}