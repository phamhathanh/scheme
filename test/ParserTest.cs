using System.Linq;
using Xunit;
using Scheme;

namespace Scheme.Tests
{
    public class ParserTest
    {
        [Fact]
        public void ItShouldParseSingleNumber() 
        {
            var parser = new Parser("1");
            Assert.True(parser.Parse().First().ToString() == "1");
        }

        [Fact]
        public void ItShouldParseSingleString() 
        {
            var parser = new Parser("\"abc\"");
            Assert.True(parser.Parse().First().ToString() == "\"abc\"");
        }
    }
}
