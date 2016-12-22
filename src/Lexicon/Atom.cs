using System;

namespace Scheme.Lexicon
{
    internal class Atom : Object
    {
        public object Value { get; }

        public Atom(object value)
        {
            Value = value;
        }

        public static Atom Parse(string input)
        {
            double number;
            bool isNumber = Double.TryParse(input, out number);
            if (isNumber)
                return new Literal(number);

            bool isString = input[0] == '"' && input[input.Length - 1] == '"';
            if (isString)
                return new Literal(input);

            bool isValidIdentifier = true;
            // TODO: validate.
            if (isValidIdentifier)
                return Identifier.FromString(input);

            throw new FormatException();
        }

        public override string ToString()
            => Value.ToString();
    }
}