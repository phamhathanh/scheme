using System;

namespace Scheme.Storage
{
    public sealed class String : Atom
    {
        private readonly string value;
        
        public String(string value)
        {
            this.value = value;
        }

        public override string ToString()
            => $"\"{value}\"";
    }
}