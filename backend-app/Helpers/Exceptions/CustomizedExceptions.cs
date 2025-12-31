namespace backend_app.Helpers.Exceptions;

public class CustomizedExceptions
{
    public class UserNotFoundException : BaseCustomException
    {
        public override int StatusCode => 404;

        public UserNotFoundException(string parameter)
            : base("User not found", $"User not found for operation: {parameter}")
        {
        }
    }

    public class EmailAlreadyExistException : BaseCustomException
    {
        public override int StatusCode => 409;

        public EmailAlreadyExistException(string parameter)
            : base("Try registering a different email address", $"Email already registered: {parameter}")
        {
        }
    }

    public class InvalidCodeException : BaseCustomException
    {
        public override int StatusCode => 404;

        public InvalidCodeException(string parameter)
            : base("Invalid or expired code", $"Invalid code: {parameter}")
        {
        }
    }

    public class ExpiredCodeException : BaseCustomException
    {
        public override int StatusCode => 404;

        public ExpiredCodeException(string parameter)
            : base("Invalid or expired code", $"Code has expired: {parameter}")
        {
        }
    }

    public class UserCodeNotFoundException : BaseCustomException
    {
        public override int StatusCode => 404;

        public UserCodeNotFoundException(string parameter)
            : base("Invalid or expired code", $"User not found: {parameter}")
        {
        }
    }

    public class CodeNotFoundException : BaseCustomException
    {
        public override int StatusCode => 404;

        public CodeNotFoundException(string parameter)
            : base("Invalid or expired code", $"Code not found: {parameter}")
        {
        }
    }
}
