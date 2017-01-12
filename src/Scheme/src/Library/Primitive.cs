using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Scheme.Storage;

namespace Scheme.Library
{
    internal static class Primitive
    {
        public static Dictionary<Symbol, Object> Macros
            => macros.ToDictionary(kvp => new Symbol(kvp.Key),
                            kvp => (Object)new Macro(kvp.Value));

        private static Dictionary<string, Macro.Transformation> macros =
                    new Dictionary<string, Macro.Transformation>
                    {
                        ["quote"] = Quote,
                        ["define"] = Define,
                        ["set!"] = Set,
                        ["if"] = If,
                        ["define-syntax"] = DefineSyntax,
                        ["lambda"] = Lambda,
                        ["let"] = Let,
                        ["begin"] = Begin
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

            var variable = argsArray[0];
            if (!(variable is Symbol))
                throw new SyntaxException("Can only set! variable.");

            var value = argsArray[1].Evaluate(env);
            env.LookUp((Symbol)variable).Assign(value);
            return null;
        }

        private static Object If(IEnumerable<Object> data, Environment env)
        {
            var argsArray = data.ToArray();
            int n = argsArray.Length;
            if (n < 2 || n > 3)
            {
                var message = $"Wrong number of arguments: 2 or 3 expected instead of {n}.";
                throw new SyntaxException(message);
            }
            var test = argsArray[0];
            var consequent = argsArray[1];
            var alternate = argsArray[2];
            bool isTrue = test.Evaluate(env) != Boolean.FALSE;
            if (isTrue)
                return consequent.Evaluate(env);
            if (n == 2)
                return null;
            // Should be unspecified.
            Debug.Assert(n == 3);
            return alternate.Evaluate(env);
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
            var syntaxRules = ((ConsCell)transformerSpec).GetListItems().Skip(2);
            var ruleSymbols = new List<IEnumerable<Symbol>>();
            var ruleTemplates = new List<Object>();
            foreach (var syntaxRule in syntaxRules)
            {
                var items = ((ConsCell)syntaxRule).GetListItems();
                var pattern = items.First();
                var symbols = from item in ((ConsCell)pattern).GetListItems().Skip(1)
                              select (Symbol)item;
                // TODO: Validate: 2 args only.
                var template = items.ElementAt(1);

                ruleSymbols.Add(symbols);
                ruleTemplates.Add(template);
            }

            var macro = new Macro((_data, _env) =>
            {
                // Hack: Match by number of args.
                var __data = _data.ToArray();
                var numberOfSymbols = __data.Length;
                foreach (var ruleSymbol in ruleSymbols)
                {
                    var symbols = ruleSymbol.ToArray();
                    if (symbols.Length == numberOfSymbols)
                    {
                        var template = ruleTemplates[ruleSymbols.IndexOf(ruleSymbol)];
                        var rules = symbols.Zip(__data, (s, a) => new { Key = s, Value = a })
                                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                        return Replace(template, rules).Evaluate(_env);
                    }
                    var elipsis = new Symbol("...");
                    if (numberOfSymbols >= symbols.Length - 1 && symbols.Length > 0 && symbols.Last().Equals(elipsis))
                    {
                        int numberOfExtraSymbols = numberOfSymbols - symbols.Length + 1;
                        var expandedSymbols = from index in Enumerable.Range(0, numberOfExtraSymbols)
                                              select new Symbol("_" + index);
                                              // Should somehow be unique.
                        symbols = symbols.Take(symbols.Length - 1).Concat(expandedSymbols).ToArray();
                        var template = ruleTemplates[ruleSymbols.IndexOf(ruleSymbol)];
                        var expandedTemplate = ReplaceElipsis(template, numberOfExtraSymbols);
                        var rules = symbols.Zip(__data, (s, a) => new { Key = s, Value = a })
                                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                        return Replace(expandedTemplate, rules).Evaluate(_env);
                    }
                }
                throw new SyntaxException($"No syntax defined for {numberOfSymbols} args.");
            });
            env.AddBinding((Symbol)keyword, macro);
            return null;
        }

        private static Object ReplaceElipsis(Object template, int numberOfExtraSymbols)
        {
            var expandedSymbols = from index in Enumerable.Range(0, numberOfExtraSymbols)
                                    select new Symbol("_" + index);
                                    // Should be unique.
            var expandedSymbolList = CreateList(expandedSymbols);
            var expansionRule = new Dictionary<Symbol, Object>
            {
                [new Symbol("...")] = expandedSymbolList
            };
            return ReplaceElipsis(template, expansionRule);
        }

        private static ConsCell CreateList(IEnumerable<Object> objects)
        {
            if (objects.Any())
                return new ConsCell(objects.First(), CreateList(objects.Skip(1)));
            return ConsCell.Nil;
        }

        private static Object ReplaceElipsis(Object datum, Dictionary<Symbol, Object> rules)
        {
            if (datum is ConsCell)
            {
                if (datum == ConsCell.Nil)
                    return datum;

                var consCell = (ConsCell)datum;
                if (consCell.Car.ToString() == "...")
                    return ReplaceElipsis(consCell.Car, rules);
                return new ConsCell(ReplaceElipsis(consCell.Car, rules), ReplaceElipsis(consCell.Cdr, rules));
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

        private static Object Let(IEnumerable<Object> data, Environment env)
        // TODO: Replace by Scheme code.
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

        private static Object Begin(IEnumerable<Object> data, Environment env)
        // TODO: Replace by Scheme code.
        {
            var body = data;
            Object result = null;
            foreach (var statement in body)
                result = statement.Evaluate(env);
            return result;
        }
    }
}