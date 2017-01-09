using System.Collections.Generic;
using System.Linq;
using Scheme.Storage;

namespace Scheme.Library
{
    internal static class Primitive
    {
        public static Dictionary<Symbol, Object> Procedures
            => procedures.ToDictionary(kvp => new Symbol(kvp.Key),
                            kvp => (Object)new Macro(kvp.Value));

        private static Dictionary<string, Macro.Transformation> procedures =
                    new Dictionary<string, Macro.Transformation>
                    {
                        ["quote"] = Quote,
                        ["define"] = Define,
                        ["set!"] = Set,
                        ["let"] = Let,
                        ["define-syntax"] = DefineSyntax,
                        ["lambda"] = Lambda
                    };

        private static Object Quote(IEnumerable<Object> data, Environment env)
        {
            var argsArray = data.ToArray();
            ValidateArgCount(1, argsArray.Length);

            return argsArray[0];
        }

        private static void ValidateArgCount(int expected, int actual)
        {
            if (actual == expected)
                return;
            var message = $"Wrong number of arguments: {expected} expected instead of {actual}.";
            throw new System.ArgumentException(message);
        }

        private static Object Define(IEnumerable<Object> data, Environment env)
        {
            var argsArray = data.ToArray();
            ValidateArgCount(2, argsArray.Length);
            // TODO: Other forms.
            // TODO: Ensure this being called at the top only.

            var variable = argsArray[0];
            if (!(variable is Symbol))
                throw new SyntaxException("Can only define variable.");

            var value = argsArray[1].Evaluate(env);
            env.AddBinding((Symbol)variable, value);
            return null;
        }

        private static Object Set(IEnumerable<Object> data, Environment env)
        {
            var argsArray = data.ToArray();
            ValidateArgCount(2, argsArray.Length);
            // TODO: Other forms.
            // TODO: Ensure this being called at the top only.

            var variable = argsArray[0];
            if (!(variable is Symbol))
                throw new SyntaxException("Can only set! variable.");

            var value = argsArray[1].Evaluate(env);
            env.LookUp((Symbol)variable).Assign(value);
            return null;
        }

        private static Object Let(IEnumerable<Object> data, Environment env)
        {
            var bindings = (ConsCell)data.First();
            var body = data.Skip(1);
            // Must have at least one.

            var newEnvironment = new Environment(env);
            foreach (var binding in bindings.GetListItems())
            {
                var items = ((ConsCell)binding).GetListItems().ToArray();
                var variable = (Symbol)items[0];
                var init = items[1].Evaluate(env);
                newEnvironment.AddBinding(variable, init);
            }
            Object result = null;
            foreach (var statement in body)
                result = statement.Evaluate(newEnvironment);
            return result;
        }

        private static Object DefineSyntax(IEnumerable<Object> data, Environment env)
        {
            var argsArray = data.ToArray();
            ValidateArgCount(2, argsArray.Length);
            // TODO: Other forms.
            // TODO: Ensure this being called at the top only.

            var keyword = argsArray[0];
            if (!(keyword is Symbol))
                throw new SyntaxException("Can only define variable.");

            var transformerSpec = argsArray[1];
            var syntaxRule = ((ConsCell)transformerSpec).GetListItems().ElementAt(2);
            // 3, 4, 5 are also rules.
            var pattern = ((ConsCell)syntaxRule).GetListItems().First();

            var symbols = from item in ((ConsCell)((ConsCell)pattern).Cdr).GetListItems()
                          select (Symbol)item;
            // Naive case.
            var template = ((ConsCell)syntaxRule).GetListItems().ElementAt(1);

            var macro = new Macro((_data, _env) =>
            {
                var rules = symbols.Zip(_data, (s, a) => new { Key = s, Value = a })
                                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                return Replace(template, rules).Evaluate(_env);
            });
            env.AddBinding((Symbol)keyword, macro);
            return null;
        }

        private static Object Replace(Object datum, Dictionary<Symbol, Object> rules)
        {
            if (datum is ConsCell)
            {
                if (datum == ConsCell.Nil)
                    return datum;
                var consCell = (ConsCell)datum;
                return new ConsCell(Replace(consCell.Car, rules), Replace(consCell.Cdr, rules));
            }
            if (datum is Symbol)
            {
                var symbol = (Symbol)datum;
                if (rules.ContainsKey(symbol))
                    return rules[symbol];
                // TODO: Deep.
            }
            return datum;
        }

        private static Object Lambda(IEnumerable<Object> data, Environment env)
        {
            var argsArray = data.ToArray();
            if (argsArray.Length < 2)
                throw new System.ArgumentException(
                    $"Wrong number of arguments: At least 2 expected instead of {argsArray.Length}.");

            var formals = data.First();
            var body = data.Skip(1);

            // Assuming formal list only.
            // TODO: consider other forms.
            // TODO: validate if items are identifiers.
            var symbols = (from item in ((ConsCell)formals).GetListItems()
                           select (Symbol)item).ToArray();

            return new Procedure(_args =>
            {
                // TODO: Validate many things...
                var lambdaEnv = new Environment(env);
                for (int i = 0; i < symbols.Length; i++)
                    lambdaEnv.AddBinding(symbols[i], _args.ElementAt(i));
                Object result = null;
                foreach (var expression in body)
                    result = expression.Evaluate(lambdaEnv);
                return result;
            });
        }
    }
}