using System;

namespace Scheme.Storage
{
    internal sealed class Number : Atom
    {
        public Number(double value) : base(value)
        { }
    }
}