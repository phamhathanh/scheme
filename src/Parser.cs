using System.Collections.Generic;
using System.Text.RegularExpressions;
using Scheme.Lexicon;

namespace Scheme
{
    internal class Parser
    {
        private readonly string source;
        private Queue<string> tokens;

        public Parser(string source)
        {
            this.source = source;
        }

        public Pair Parse()
        {
            var tokens = Tokenize(source);
            this.tokens = new Queue<string>(tokens);
            return Read();
        }

        private string[] Tokenize(string source)
        {
            var openParens = new Regex(@"\(");
            var closeParens = new Regex(@"\)");
            var multispaces = new Regex(@"\s+");
            string temp = source;
            temp = openParens.Replace(temp, " ( ");
            temp = closeParens.Replace(temp, " ) ");
            temp = temp.Trim();
            return multispaces.Split(temp);
        }

        private Pair Read()
        {
            if (tokens.Count == 0)
                return Pair.Nil;
            var token = tokens.Dequeue();
            Object car, cdr;
            if (token == "(")
            {
                car = Read();
                cdr = Read();
                return new Pair(car, cdr);
            }
            if (token == ")")
                return Pair.Nil;

            car = Atom.Parse(token);
            cdr = Read();
            return new Pair(car, cdr);
        }
    }
}