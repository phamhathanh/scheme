using Xunit;

namespace Scheme.Tests
{
    public class BooleanTest
    {
        [Fact]
        public void NotTest() 
        {
            var source = "(not #t)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "#f";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsBooleanTest() 
        {
            var source = "(boolean? #f)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "#t";
            Assert.Equal(expected, result);
        }
    }
}
