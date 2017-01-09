using System;

namespace Scheme.REPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var interpreter = new Interpreter();
            while (true)
            {
                Console.WriteLine();
                Console.Write("> ");
                var source = Console.ReadLine();
                try
                {
                    var result = interpreter.Interpret(source);
                    if (result != null)
                        Console.WriteLine($"=> {result}");
                    // TODO: Preserve program state.
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.GetType() + ": " + exception.Message);
                    return;
                    // TODO: Do something about the invalid state.
                    //       Rollback for example.
                }
            }
        }
    }
}
