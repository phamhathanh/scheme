using System;

namespace Scheme.Storage
{
    internal abstract class Atom : Object
    {
        public static Atom Parse(string input)
        {
            double number;
            bool isNumber = double.TryParse(input, out number);
            if (isNumber)
                return new Number(number);

            bool isString = input.Length > 2 && input[0] == '"' && input[input.Length - 1] == '"';
            if (isString)
                return new String(input.Substring(1, input.Length - 2));

            if (Symbol.IsValid(input))
                return new Symbol(input);

            throw new SyntaxException("Incorrect atom format.");
        }
    }
}