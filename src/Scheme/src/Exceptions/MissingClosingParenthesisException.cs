using System;

public class MissingClosingParenthesisException : SyntaxException
{
    public MissingClosingParenthesisException() : base("Missing closing parenthesis.")
    { }
}