using System.Collections.Generic;
using Scheme.Storage;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Scheme
{
    public class Interpreter
    {
        public string Interpret(string source)
        {
            var parser = new Parser();
            var data = parser.Parse(source);
            var globalEnvironment = new Environment(StandardLibrary.Procedures, null);
            Object result = null;
            foreach (var datum in data)
                result = datum.Evaluate(globalEnvironment);
            return result.ToString();
        }
    }
}