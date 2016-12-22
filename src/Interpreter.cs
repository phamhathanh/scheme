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
            {
                var expression = (ConsCell)datum;
                return expression.Evaluate(globalEnvironment);
            }
            return datum;
        }
    }
}