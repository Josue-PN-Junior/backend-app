namespace backend_app.Helpers.Exceptions;

public class TooManyRequestExeption : BaseCustomException
{
    public override int StatusCode => 429;
    public TooManyRequestExeption(
        string parameter,
        string? detailedMessage = null
    ) : base("Too Many Request", detailedMessage ?? $"TooManyRequests: {parameter}")
    {
    }
}
