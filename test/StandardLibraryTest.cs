using System.Linq;
using Xunit;
using Scheme;

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
    }
}
