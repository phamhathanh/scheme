using System;

namespace Scheme
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var source = "abc ((1 \"2\") 3 4)";
            var parser = new Parser(source);
            Console.WriteLine(parser.Parse());
            //var data = Read(tokens);
            //Console.WriteLine(data.ToString());
            //Interpret(data);
        }
    }
}
