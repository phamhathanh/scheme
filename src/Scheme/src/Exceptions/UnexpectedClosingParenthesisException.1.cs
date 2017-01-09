using System;

public class UnexpectedClosingParenthesisException : SyntaxException
{
    public UnexpectedClosingParenthesisException() : base("Unexpected closing parenthesis.")
    { }
}