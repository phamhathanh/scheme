using System;
using Xunit;

namespace Scheme.Tests
{
    public class StandardLibraryTest
    {
        [Fact]
        public void PlusTest() 
        {
            var source = "(+ 1 2)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "3";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void QuoteTest() 
        {
            var source = "(quote (1 2))";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "(1 2)";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsPairTest() 
        {
            var source = "(pair? (quote (1 2)))";
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
    }
}
