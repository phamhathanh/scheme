using System;
using Xunit;

namespace Scheme.Tests
{
    public class DerivedExpressionTest
    {
        [Fact]
        public void AndTest() 
        {
            var source = "(and 1 2 'c '(f g))";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "(f g)";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OrTest()
        {
            var source = "(or #f #f #f)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "#f";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BeginTest()
        {
            var source =
@"(define x 0)
    (and (= x 0)
         (begin (set! x 5)
                (+ x 1)))";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "6";
            Assert.Equal(expected, result);
        }
    }
}
