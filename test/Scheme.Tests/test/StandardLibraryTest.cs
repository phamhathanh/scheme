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
            var result = Interpreter.Interpret(source);
            var expectedResult = "3";
            Assert.True(result == expectedResult);
        }

        [Fact]
        public void QuoteTest() 
        {
            var source = "(quote (1 2))";
            var result = Interpreter.Interpret(source);
            var expectedResult = "(1 2)";
            Assert.True(result == expectedResult);
        }

        [Fact]
        public void IsPairTest() 
        {
            var source = "(pair? (quote (1 2)))";
            var result = Interpreter.Interpret(source);
            var expectedResult = "#t";
            Assert.True(result == expectedResult);
        }

        [Fact]
        public void IsPairTestFail() 
        {
            var source = "(pair? 2)";
            var result = Interpreter.Interpret(source);
            var expectedResult = "#f";
            Assert.True(result == expectedResult);
        }

        [Fact]
        public void IsPairWrongArgs() 
        {
            var source = "(pair? 2 3)";
            Assert.Throws<ArgumentException>(() => Interpreter.Interpret(source));
        }
    }
}
