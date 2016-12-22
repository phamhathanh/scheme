using System;
using System.Collections.Generic;

namespace Scheme.Storage
{
    internal class Environment
    {
        private readonly IDictionary<Identifier, Object> bindings;
        private readonly Environment parent;

        public Environment(IDictionary<Identifier, Object> bindings, Environment parent)
        {
            this.bindings = bindings;
            this.parent = parent;
        }

        public Object LookUp(Identifier identifier)
        {
            if (bindings.ContainsKey(identifier))
                return bindings[identifier];
            if (parent != null)
                return parent.LookUp(identifier);
            throw new ArgumentException($"Unbounded identifier: {identifier}");
        }
    }
}