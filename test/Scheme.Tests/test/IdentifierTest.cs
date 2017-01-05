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
            var expectedResult = "abc$%^";
            Assert.True(result == expectedResult);
        }

        [Fact]
        public void ShouldNotBeAnIdentifier() 
        {
            var source = "(quote .)";
            var interpreter = new Interpreter();
            Assert.Throws<SyntaxException>(() => interpreter.Interpret(source));
            // TODO: fix this when pair notion is implemented.
        }
    }
}
