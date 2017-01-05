using System.Collections.Generic;
using Scheme.Storage;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Scheme
{
    internal class Parser
    {
        private Queue<string> tokens = new Queue<string>();

        public IEnumerable<Object> Parse(string source)
        {
            Debug.Assert(tokens.Count == 0);
            tokens = new Queue<string>(Tokenize(source));
            var topLevel = Read();
            while (topLevel != Nil.Instance)
            {
                yield return ((ConsCell)topLevel).Car;
                topLevel = ((ConsCell)topLevel).Cdr;
            }
            // Needs rework.
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

        private Object Read()
        {
            if (tokens.Count == 0)
                return Nil.Instance;
            var token = tokens.Dequeue();
            Object car, cdr;
            if (token == "(")
            {
                car = Read();
                cdr = Read();
                return new ConsCell(car, cdr);
            }
            if (token == ")")
                return Nil.Instance;

            car = Atom.Parse(token);
            cdr = Read();
            return new ConsCell(car, cdr);
        }
    }
}