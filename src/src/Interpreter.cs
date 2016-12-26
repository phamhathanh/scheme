using System.Diagnostics;
using Scheme.Storage;

namespace Scheme
{
    public static class Interpreter
    {
        public static string Interpret(string source)
        {
            var parser = new Parser(source);
            var data = parser.Parse();
            Object result = null;
            foreach (var datum in data)
                result = Evaluator.Evaluate(datum);
            return result.ToString();
        }
    }
}