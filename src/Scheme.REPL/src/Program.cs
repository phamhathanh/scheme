using System;

namespace Scheme.REPL
{
    public class Program
    {
        private static string leftovers = "";

        public static void Main(string[] args)
        {
            var interpreter = new Interpreter();
            Console.WriteLine();
            Console.Write(" > ");
            while (true)
            {
                var source = leftovers + Console.ReadLine();
                try
                {
                    var result = interpreter.Interpret(source);
                    if (result != null)
                        Console.WriteLine($" => {result}");
                    Console.Write(" > ");
                    leftovers = "";
                }
                catch (MissingClosingParenthesisException)
                {
                    leftovers = source;
                    Console.Write($".. ");
                    // TODO: Indent.
                    continue;
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
