using System;
using System.Collections.Generic;
using Scheme.Storage;
using Object = Scheme.Storage.Object;
using Environment = Scheme.Storage.Environment;

namespace Scheme
{
    internal static class Interpreter
    {
        public static Object Interpret(Object datum)
        {
            var globalEnvironment = new Environment(StandardLibrary.Procedures, null);

            if (datum is ConsCell)
                return Evaluate((ConsCell)datum, globalEnvironment);
            return datum;
        }

        private static Storage.Object Evaluate(ConsCell expression, Environment environment) {
            var keyword = expression.Car;
            if (keyword is ConsCell && keyword != ConsCell.Nil)
                keyword = Evaluate((ConsCell)keyword, environment);

            bool isAKeyword = keyword is Identifier;
            if (!isAKeyword)
                throw new ArgumentException($"Syntax error: {keyword} is not a keyword.");

            var _object = environment.LookUp((Identifier)keyword);
            if (_object is Procedure)
            {
                var procedure = (Procedure)_object;
                var args = expression.Cdr;
                return procedure.Invoke(args, environment);
            }
            
            throw new NotImplementedException();
        }
    }
}