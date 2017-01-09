using System.Collections.Generic;
using Scheme.Storage;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;

namespace Scheme
{
    internal class Parser
    {
        private Queue<string> tokens = new Queue<string>();
        private int openingParensCount = 0;

        public IEnumerable<Object> Parse(string source)
        {
            Debug.Assert(tokens.Count == 0);
            tokens = new Queue<string>(Tokenize(source));
            openingParensCount = 0;

            var topLevelList = Read();

            if (openingParensCount < 0)
                throw new UnexpectedClosingParenthesisException();
            if (openingParensCount > 0)
                throw new MissingClosingParenthesisException();

            return topLevelList.GetListItems();
        }

        private string[] Tokenize(string source)
        {
            var openParens = new Regex(@"\(");
            var closeParens = new Regex(@"\)");
            var quotes = new Regex("'");
            var spaces = new Regex(@"\s+");
            string temp = source;
            temp = openParens.Replace(temp, " ( ");
            temp = closeParens.Replace(temp, " ) ");
            temp = quotes.Replace(temp, " ' ");
            temp = temp.Trim();
            return spaces.Split(temp);
        }

        private ConsCell Read()
        {
            if (tokens.Count == 0)
                return ConsCell.Nil;
            
            var token = tokens.Dequeue();
            if (token == "(")
            {
                openingParensCount++;
                var car = Read();
                var cdr = Read();
                return new ConsCell(car, cdr);
            }
            if (token == ")")
            {
                openingParensCount--;
                return ConsCell.Nil;
            }
            if (token == "'")
            {
                var caar = Atom.Parse("quote");
                var cadr = Read();
                var car = new ConsCell(caar, cadr);
                var cdr = Read();
                return new ConsCell(car, cdr);
            }
            {
                var car = Atom.Parse(token);
                var cdr = Read();
                return new ConsCell(car, cdr);
            }
        }
    }
}