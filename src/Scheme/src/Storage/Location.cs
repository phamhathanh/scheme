using System;

namespace Scheme.Storage
{
    internal sealed class Location
    {
        public Object Object { get; private set; }

        public Location(Object _object)
        {
            Object = _object;
        }

        public void Assign(Object _object)
            => Object = _object;
    }
}