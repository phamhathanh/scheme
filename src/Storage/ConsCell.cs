using System;

namespace Scheme.Storage
{
    internal sealed class ConsCell : Object
    {
        public Object Car { get; private set; }
        public Object Cdr { get; private set; }

        public ConsCell(Object car, Object cdr)
        {
            // Check if null.
            Car = car;
            Cdr = cdr;
        }

        public override Object Evaluate(Environment environment)
        {
            var keyword = Car;
            if (keyword is ConsCell)
            {
                var expression = (ConsCell)keyword;
                keyword = expression.Evaluate(environment);
            }

            bool isAKeyword = keyword is Identifier;
            if (!isAKeyword)
                throw new ArgumentException($"Syntax error: {keyword} is not a keyword.");

            var _object = environment.LookUp((Identifier)keyword);
            if (_object is Procedure)
            {
                var procedure = (Procedure)_object;
                var args = Cdr;
                return procedure.Invoke(args, environment);
            }
            
            throw new NotImplementedException();
        }

        public override string ToString()
            => $"({Car} . {Cdr})";
    }
}