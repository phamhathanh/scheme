using System;

public class SyntaxException : Exception
{
    public override string Message { get; }

    public SyntaxException(string message)
    {
        Message = message;
    }
}