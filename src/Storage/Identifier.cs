using System.Collections.Generic;

namespace Scheme.Storage
{
    internal sealed class Identifier : Atom
    {
        private static Dictionary<string, Identifier> pool = new Dictionary<string, Identifier>();

        private Identifier(string value) : base(value)
        { }

        public static Identifier FromString(string input)
        {
            bool exists = pool.ContainsKey(input);
            if (!exists)
                pool.Add(input, new Identifier(input));
            return pool[input];
        }
    }
}