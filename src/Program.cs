using System;

namespace Scheme
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var source = "abc ((1 \"2\") 3 4)";
            var parser = new Parser(source);
            var data = parser.Parse();
            Console.WriteLine(data);
            //Interpret(data);
        }
    }
}
