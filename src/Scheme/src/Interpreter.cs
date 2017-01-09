using Scheme.Storage;
using System.Linq;

namespace Scheme
{
    public class Interpreter
    {
        public string Interpret(string source)
        {
            var parser = new Parser();
            var data = parser.Parse(source);
            var globalEnvironment = new Environment(null);
            var library = Library.Primitive.Procedures
                            .Concat(Library.Numbers.Procedures)
                            .Concat(Library.PairAndList.Procedures);
            foreach (var procedure in library)
                globalEnvironment.AddBinding(procedure.Key, procedure.Value);
            
            Object result = null;
            foreach (var datum in data)
                result = datum.Evaluate(globalEnvironment);
            return result?.ToString();
        }
    }
}