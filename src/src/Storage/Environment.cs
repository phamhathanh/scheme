using System;
using System.Collections.Generic;

namespace Scheme.Storage
{
    internal class Environment
    {
        private readonly IDictionary<Symbol, Object> bindings;
        private readonly Environment parent;

        public Environment(IDictionary<Symbol, Object> bindings, Environment parent)
        {
            this.bindings = bindings;
            this.parent = parent;
        }

        public Object LookUp(Symbol symbol)
        {
            if (bindings.ContainsKey(symbol))
                return bindings[symbol];
            if (parent != null)
                return parent.LookUp(symbol);
            throw new ArgumentException($"Unbounded identifier: {symbol}");
        }
    }
}