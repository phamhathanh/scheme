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
        public void LetTest() 
        {
            var source =
                @"(let ((x 15))
                    x)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "15";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MacroTest() 
        {
            var source =
                @"(define-syntax one
                    (syntax-rules ()
                      ((one)
                       1)))
                  (one)";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "1";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MacroWithVariableTest() 
        {
            var source =
                @"(define-syntax plus-one
                    (syntax-rules ()
                      ((plus-one x)
                       (+ x 1))))
                  (plus-one (+ 5 4))";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "10";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MacroWithVariableTest2() 
        {
            var source =
                @"(let ((x 1) (y 2))
                    (define-syntax swap!
                      (syntax-rules ()
                        ((swap! a b)
                         (let ((tmp a))
                           (set! a b)
                           (set! b tmp)))))
                  (swap! x y)
                  (cons x y))";
            var interpreter = new Interpreter();
            var result = interpreter.Interpret(source);
            var expected = "(2 . 1)";
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
