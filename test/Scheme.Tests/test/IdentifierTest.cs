using Xunit;

namespace Scheme.Tests
{
    public class IdentifierTest
    {
        [Fact]
        public void ShouldBeAnIdentifier() 
        {
            var source = "(quote abc$%^)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "abc$%^";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldNotBeAnIdentifier() 
        {
            var source = "(quote .)";
            var interpreter = new Interpreter();
            Assert.Throws<SyntaxException>(() => interpreter.Interpret(source));
            // TODO: fix this when pair notion is implemented.
        }

        [Fact]
        public void ShouldDoNothing() 
        {
            var source = "";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            Assert.Null(result);
        }
    }
}
