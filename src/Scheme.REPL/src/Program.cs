using System;
using Scheme;

namespace Scheme.REPL
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
                try
                {
                    var result = Interpreter.Interpret(source);
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
