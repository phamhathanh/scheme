using Scheme.Storage;
using System.Linq;

namespace Scheme
{
    public class Interpreter
    {
        private readonly Parser parser;
        private readonly Environment global;

        public Interpreter()
        {
            parser = new Parser();
            global = new Environment(null);
            var library = Library.Primitive.Procedures
                            .Concat(Library.Numbers.Procedures)
                            .Concat(Library.PairAndList.Procedures);
            foreach (var procedure in library)
                global.AddBinding(procedure.Key, procedure.Value);
        }

        public string Interpret(string source)
        {
            var data = parser.Parse(source);
            Object result = null;
            foreach (var datum in data)
                result = datum.Evaluate(global);
            return result?.ToString();
        }
    }
}