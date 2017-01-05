using System;
using Xunit;

namespace Scheme.Tests
{
    public class StandardLibraryTest
    {
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
        public void DefineTest() 
        {
            var source = "(define abc 5) abc";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "5";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PlusTest() 
        {
            var source = "(+ 1 2)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "3";
            Assert.Equal(expected, result);
        }
    }
}
