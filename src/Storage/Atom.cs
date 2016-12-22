using System;

namespace Scheme.Storage
{
    internal abstract class Atom : Object
    {
        public static Atom Parse(string input)
        {
            double number;
            bool isNumber = Double.TryParse(input, out number);
            if (isNumber)
                return new Number(number);

            bool isString = input[0] == '"' && input[input.Length - 1] == '"';
            if (isString)
                return new String(input.Substring(1, input.Length - 2));

            bool isValidIdentifier = true;
            // TODO: validate.
            if (isValidIdentifier)
                return Identifier.FromString(input);

            throw new FormatException();
        }
    }
}