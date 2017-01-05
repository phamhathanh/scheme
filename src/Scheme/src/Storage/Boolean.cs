using System;

namespace Scheme.Storage
{
    internal sealed class Boolean : Atom
    {
        public static readonly Boolean TRUE = new Boolean(true),
                                    FALSE = new Boolean(false);

        private readonly bool value;
        
        private Boolean(bool value)
        {
            this.value = value;
        }

        public static Boolean FromString(string input)
        {
            switch (input)
            {
                case "#t":
                case "#true":
                    return TRUE;
                case "#f":
                case "#false":
                    return FALSE;
                default:
                    throw new FormatException();
            }
        }

        public static Boolean FromBool(bool input)
            => (input)? TRUE : FALSE;

        public override sealed Object Evaluate(Environment environment)
            => this;

        public override sealed string ToString()
            => (value)? "#t" : "#f";
    }
}