using Xunit;

namespace Scheme.Tests
{
    public class NumberTest
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
        public void MinusTest1Arg() 
        {
            var source = "(- 10)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "-10";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MinusTest2Arg() 
        {
            var source = "(- 10 12)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "-2";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MultiplyTest() 
        {
            var source = "(* 3 2)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "6";
            Assert.Equal(expected, result);
        }
    }
}
