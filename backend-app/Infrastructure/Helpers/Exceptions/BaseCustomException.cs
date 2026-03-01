namespace backend_app.Helpers.Exceptions;

public abstract class BaseCustomException : Exception
{
    public abstract int StatusCode { get; }
    public string DetailedMessage { get; }
    public string ProductionMessage { get; }

    protected BaseCustomException(string productionMessage, string detailedMessage)
        : base(productionMessage)
    {
        ProductionMessage = productionMessage;
        DetailedMessage = detailedMessage;
    }
}
