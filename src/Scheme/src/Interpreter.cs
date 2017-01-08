using Scheme.Storage;

namespace Scheme
{
    public class Interpreter
    {
        public string Interpret(string source)
        {
            var parser = new Parser();
            var data = parser.Parse(source);
            var globalEnvironment = new Environment(null);
            foreach (var procedure in StandardLibrary.Procedures)
                globalEnvironment.AddBinding(procedure.Key, procedure.Value);
            
            Object result = null;
            foreach (var datum in data)
                result = datum.Evaluate(globalEnvironment);
            return result?.ToString();
        }
    }
}