using System;
using System.Linq;

namespace Scheme
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var source = "((lambda (x) x) 2)";
            var parser = new Parser(source);
            var data = parser.Parse().ToArray();
            foreach (var datum in data)
                Console.Write(datum + " ");
            Console.WriteLine();

            try
            {
                Object result = null;
                foreach (var datum in data)
                    result = Interpreter.Interpret(datum);
                Console.WriteLine($"=> {result}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
            }
        }
    }
}
