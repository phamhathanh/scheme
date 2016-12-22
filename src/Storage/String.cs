using System;

namespace Scheme.Storage
{
    internal sealed class String : Atom
    {
        public String(string value) : base(value)
        { }

        public override string ToString()
            => $"\"{Value}\"";
    }
}