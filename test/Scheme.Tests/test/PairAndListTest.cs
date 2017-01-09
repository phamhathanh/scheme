using System;
using Xunit;

namespace Scheme.Tests
{
    public class PairAndListTest
    {
        [Fact]
        public void IsPairTest() 
        {
            var source = "(pair? '(1 2))";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "#t";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsPairTestFail() 
        {
            var source = "(pair? 2)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "#f";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsPairWrongArgs() 
        {
            var source = "(pair? 2 3)";
            var interpreter = new Interpreter();
            Assert.Throws<ArgumentException>(() => interpreter.Interpret(source));
        }

        [Fact]
        public void ConsTest() 
        {
            var source = "(cons 2 3)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "(2 . 3)";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CarTest() 
        {
            var source = "(car '(a b c))";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "a";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CdrTest() 
        {
            var source = "(cdr '(a b c))";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "(b c)";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsNullTest() 
        {
            var source = "(null? '())";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "#t";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsListTest() 
        {
            var source = "(list? '(a b c))";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "#t";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NilShouldBeList() 
        {
            var source = "(list? '())";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "#t";
            Assert.Equal(expected, result);
        }
    }
}
