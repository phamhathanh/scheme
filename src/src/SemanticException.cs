using System;

public class SemanticException : Exception
{
    public override string Message { get; }

    public SemanticException(string message)
    {
        Message = message;
    }
}