using System;
using System.Collections.Generic;

namespace Scheme.Storage
{
    internal sealed class Identifier : Atom
    {
        private static Dictionary<string, Identifier> pool = new Dictionary<string, Identifier>();

        private readonly string value;

        private Identifier(string value)
        {
            this.value = value;
        }

        public static Identifier FromString(string input)
        {
            bool exists = pool.ContainsKey(input);
            if (!exists)
                pool.Add(input, new Identifier(input));
            return pool[input];
        }

        public override string ToString()
            => value;
    }
}