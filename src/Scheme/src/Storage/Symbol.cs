using System.Text.RegularExpressions;

namespace Scheme.Storage
{
    internal sealed class Symbol : Atom
    {
        private readonly string value;

        public Symbol(string value)
        {
            if (!Symbol.IsValid(value))
                throw new System.ArgumentException("Invalid symbol.");

            this.value = value;
        }

        public static bool IsValid(string input)
        {
            if (input == ".")
                return false;

            var letters = "A-Z a-z";
            var digits = "0-9";
            var extendedCharacters = @"! $ % & * + - . / : < = > ? @ ^ _ ~";
            var regex = new Regex($"^[{letters}{digits}{extendedCharacters}]+$");
            return regex.IsMatch(input);
        }

        public override Object Evaluate(Environment environment)
            => environment.LookUp(this);

        public override string ToString()
            => value;

        public override bool Equals(object other)
            => other is Symbol && ((Symbol)other).value == this.value;

        public override int GetHashCode()
            => value.GetHashCode();
    }
}