using System.Diagnostics;
using Scheme.Storage;

namespace Scheme
{
    public static class Interpreter
    {
        public static string Interpret(string source)
        {
            var data = Parser.Parse(source);
            Object result = null;
            foreach (var datum in data)
                result = Evaluator.Evaluate(datum);
            return result.ToString();
        }
    }
}