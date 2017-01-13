using System;

namespace Scheme.REPL
{
    public class Program
    {
        private const string HEADER = 
@"
 -----------------------
 Scheme.NET interpreter
 -----------------------
";

        private static string leftovers = "";

        public static void Main(string[] args)
        {
            Console.WriteLine(HEADER);
            Console.WriteLine();
            
            var interpreter = new Interpreter();
            Console.Write(" > ");
            while (true)
            {
                var source = leftovers + Console.ReadLine();
                try
                {
                    var result = interpreter.Interpret(source);
                    if (result != null)
                    {
                        Console.WriteLine($"=> {result}");
                        Console.WriteLine();
                    }
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
                    Console.Write(" > ");
                    leftovers = "";
                    // TODO: Do something about the invalid state.
                    //       Rollback for example.
                }
            }
        }
    }
}
