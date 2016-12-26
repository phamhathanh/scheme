using System;
using System.Linq;

namespace Scheme
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("> ");
                var source = Console.ReadLine();
                var parser = new Parser(source);
                var data = parser.Parse().ToArray();
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
                }
            }
        }
    }
}
