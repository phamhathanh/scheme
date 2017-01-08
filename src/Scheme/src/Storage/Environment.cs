using System;
using System.Collections.Generic;

namespace Scheme.Storage
{
    internal class Environment
    {
        private readonly IDictionary<Symbol, Location> bindings;
        private readonly Environment parent;

        public Environment(Environment parent)
        {
            this.parent = parent;
            this.bindings = new Dictionary<Symbol, Location>();
        }

        public void AddBinding(Symbol symbol, Object value)
            => bindings[symbol] = new Location(value);

        public Location LookUp(Symbol symbol)
        {
            if (bindings.ContainsKey(symbol))
                return bindings[symbol];
            if (parent != null)
                return parent.LookUp(symbol);
            throw new ArgumentException($"Unbounded identifier: {symbol}");
        }
    }
}