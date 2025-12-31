namespace backend_app.Core.Domain.Exceptions;

public abstract class BaseCustomException : Exception
{
    public abstract int StatusCode { get; }
    public string UserMessage { get; }
    public string DetailedMessage { get; }

    protected BaseCustomException(string userMessage, string detailedMessage)
        : base(detailedMessage)
    {
        UserMessage = userMessage;
        DetailedMessage = detailedMessage;
    }
}