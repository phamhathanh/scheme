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

            bool isString = input.Length > 2 && input[0] == '"' && input[input.Length - 1] == '"';
            if (isString)
                return new String(input.Substring(1, input.Length - 2));

            bool isValidSymbol = true;
            // TODO: validate.
            if (isValidSymbol)
                return Symbol.FromString(input);

            throw new FormatException();
        }
    }
}